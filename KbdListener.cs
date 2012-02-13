//-----------------------------------------------------------------------
// <copyright file="KbdListener.cs" author="Dominique Bijnens">
// Licenced: CPOL 
// http://www.codeproject.com/info/cpol10.aspx
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
#if windows

    using System;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.ComponentModel;

	/// <summary>
	/// The KeyboardListener is a static class that allows registering a number
	/// of event handlers that you want to get called in case some keyboard key is pressed 
	/// or released. The nice thing is that this KeyboardListener is also active in case
	/// the parent application is running in the back.
	/// </summary>
	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="SkipVerification")]
	public class KeyboardListener
	{
		#region Private declarations

		/// <summary>
		/// The Window that intercepts Keyboard messages
		/// </summary>
		private static ListeningWindow s_Listener;

		#endregion

		#region Private methods
		/// <summary>
		/// The function that will handle all keyboard activity signaled by the ListeningWindow.
		/// In this context handling means calling all registered subscribers for every key pressed / released.
		/// </summary>
		/// <remarks>
		/// Inside this method the events could also be fired by calling
		/// s_KeyEventHandler(null,new KeyEventArgs(key,msg)) However, in case one of the registered
		/// subscribers throws an exception, execution of the non-executed subscribers is cancelled.
		/// </remarks>
		/// <param name="key"></param>
		/// <param name="msg"></param>
        private void KeyHandler(ushort key, uint msg) //static
		{
			if(s_KeyEventHandler != null)
			{
				Delegate[] delegates = s_KeyEventHandler.GetInvocationList();

				foreach(Delegate del in delegates)
				{
					EventHandler sink = (EventHandler)del;

					try
					{
						// This is a static class, therefore null is passed as the object reference
						sink(null,new UniversalKeyEventArgs(key,msg));
					}

					// You can add some meaningful code to this catch block.
					catch{};
				}
			}
		}
		#endregion

		#region Public declarations

		/// <summary>
		/// An instance of this class is passed when Keyboard events are fired by the KeyboardListener.
		/// </summary>
		public class UniversalKeyEventArgs : KeyEventArgs
		{
            /// <summary>
            /// 
            /// </summary>
			public readonly uint m_Msg;

            /// <summary>
            /// 
            /// </summary>
			public readonly ushort m_Key;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="aKey"></param>
            /// <param name="aMsg"></param>
			public UniversalKeyEventArgs(ushort aKey, uint aMsg) : base((Keys)aKey)
			{
				m_Msg = aMsg;
				m_Key = aKey;
			}
		}	

		/// <summary>
		/// For every application thread that is interested in keyboard events
		/// an EventHandler can be added to this variable
		/// </summary>
        public event EventHandler s_KeyEventHandler; //static

		#endregion

		#region Public methods

        /// <summary>
        /// 
        /// </summary>
        public KeyboardListener() //static
		{
			ListeningWindow.KeyDelegate aKeyDelegate = new ListeningWindow.KeyDelegate(KeyHandler);
			s_Listener = new ListeningWindow(aKeyDelegate);
		}

		#endregion

		#region Definition ListeningWindow class
		/// <summary>
		/// A ListeningWindow object is a Window that intercepts Keyboard events.
		/// </summary>
		private class ListeningWindow : NativeWindow
		{
			#region Declarations
            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            /// <param name="msg"></param>
			public delegate void KeyDelegate( ushort key, uint msg );

            /// <summary>
            /// 
            /// </summary>
			private const int 
				WS_CLIPCHILDREN		= 0x02000000,
				WM_INPUT			= 0x00FF,
				RIDEV_INPUTSINK		= 0x00000100,
				RID_INPUT			= 0x10000003,
				RIM_TYPEKEYBOARD	= 1;

            /// <summary>
            /// 
            /// </summary>
			private uint m_PrevMessage = 0;

            /// <summary>
            /// 
            /// </summary>
			private ushort m_PrevControlKey = 0;

            /// <summary>
            /// 
            /// </summary>
			private KeyDelegate m_KeyHandler = null;
			#endregion

			#region Unsafe types
            /// <summary>
            /// 
            /// </summary>
			internal unsafe struct RAWINPUTDEV 
			{
				public ushort usUsagePage;
				public ushort usUsage;
				public uint dwFlags;
				public void* hwndTarget;
			};

            /// <summary>
            /// 
            /// </summary>
			internal unsafe struct RAWINPUTHEADER 
			{
				public uint dwType;
				public uint dwSize;
				public void* hDevice;
				public void* wParam;
			};

            /// <summary>
            /// 
            /// </summary>
			internal unsafe struct RAWINPUTHKEYBOARD 
			{
				public RAWINPUTHEADER header;
				public ushort MakeCode;
				public ushort Flags;
				public ushort Reserved;
				public ushort VKey;
				public uint Message;
				public uint ExtraInformation;

			};
			#endregion

            /// <summary>
            /// 
            /// </summary>
            /// <param name="keyHandlerFunction"></param>
			public ListeningWindow(KeyDelegate keyHandlerFunction)
			{
				m_KeyHandler = keyHandlerFunction;

				CreateParams cp = new CreateParams();

				// Fill in the CreateParams details.
				cp.Caption = "Hidden window";
				cp.ClassName = null;
				cp.X = 0x7FFFFFFF;
				cp.Y = 0x7FFFFFFF;
				cp.Height = 0;
				cp.Width = 0;
				//cp.Parent = parent.Handle;
				cp.Style = WS_CLIPCHILDREN;

				// Create the actual invisible window
				this.CreateHandle(cp);

				// Register for Keyboard notification
				unsafe
				{
					try
					{
						RAWINPUTDEV myRawDevice = new RAWINPUTDEV();
						myRawDevice.usUsagePage = 0x01;
						myRawDevice.usUsage = 0x06;
						myRawDevice.dwFlags = RIDEV_INPUTSINK;
						myRawDevice.hwndTarget = this.Handle.ToPointer();

						if (RegisterRawInputDevices(&myRawDevice, 1, (uint)sizeof(RAWINPUTDEV)) == false) 
						{
							int err = Marshal.GetLastWin32Error();
							throw new Win32Exception(err,"ListeningWindow::RegisterRawInputDevices");
						}
					}

					catch {throw;}
				}
			}
	

			#region Private methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="m"></param>
			protected override void WndProc(ref Message m)
			{
				switch (m.Msg)
				{
					case WM_INPUT:
					{
						try
						{
							unsafe
							{
								uint dwSize, receivedBytes;
								uint sizeof_RAWINPUTHEADER = (uint)(sizeof(RAWINPUTHEADER));

								// Find out the size of the buffer we have to provide
								int res = GetRawInputData(m.LParam.ToPointer(), RID_INPUT, null, &dwSize, sizeof_RAWINPUTHEADER);
						
								if(res == 0)
								{
									// Allocate a buffer and ...
									byte* lpb = stackalloc byte[(int)dwSize];

									// ... get the data
									receivedBytes = (uint)GetRawInputData((RAWINPUTHKEYBOARD*)(m.LParam.ToPointer()), RID_INPUT, lpb, &dwSize, sizeof_RAWINPUTHEADER);
									if ( receivedBytes == dwSize )
									{
										RAWINPUTHKEYBOARD* keybData = (RAWINPUTHKEYBOARD*)lpb;

										// Finally, analyze the data
										if(keybData->header.dwType == RIM_TYPEKEYBOARD)
										{
											if((m_PrevControlKey != keybData->VKey) || (m_PrevMessage != keybData->Message))
											{
												m_PrevControlKey = keybData->VKey;
												m_PrevMessage = keybData->Message;

												// Call the delegate in case data satisfies
												m_KeyHandler(keybData->VKey,keybData->Message);
											}
										}
									}
									else
									{
										string errMsg = string.Format("WndProc::GetRawInputData (2) received {0} bytes while expected {1} bytes", receivedBytes, dwSize);
										throw new Exception(errMsg);
									}
								}
								else
								{
									string errMsg = string.Format("WndProc::GetRawInputData (1) returned non zero value ({0})", res);
									throw new Exception(errMsg);
								}
							}
						}

						catch {throw;}
					}
					break;
				}

				// In case you forget this you will run into problems
				base.WndProc(ref m);
			}       
	
			#endregion
	
			#region Private external methods

			// In case you want to have a comprehensive overview of calling conventions follow the next link:
			// http://www.codeproject.com/cpp/calling_conventions_demystified.asp

			[DllImport("User32.dll",CharSet = CharSet.Ansi,SetLastError=true)]
			[return : MarshalAs(UnmanagedType.Bool)]
			internal static extern unsafe bool RegisterRawInputDevices( RAWINPUTDEV* rawInputDevices, uint numDevices, uint size);

			[DllImport("User32.dll",CharSet = CharSet.Ansi,SetLastError=true)]
			[return : MarshalAs(UnmanagedType.I4)]
			internal static extern unsafe int GetRawInputData( void* hRawInput,
				uint uiCommand,
				byte* pData,
				uint* pcbSize,
				uint cbSizeHeader
				);

			#endregion
		}
		#endregion
	}
#endif
}
