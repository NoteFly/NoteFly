using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SimplePlainNote
{
    class CrashMiniDump
        {
            internal enum MINIDUMP_TYPE
            {
                MiniDumpNormal = 0x00000000,
                MiniDumpWithDataSegs = 0x00000001,
                MiniDumpWithFullMemory = 0x00000002,
                MiniDumpWithHandleData = 0x00000004,
                MiniDumpFilterMemory = 0x00000008,
                MiniDumpScanMemory = 0x00000010,
                MiniDumpWithUnloadedModules = 0x00000020,
                MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
                MiniDumpFilterModulePaths = 0x00000080,
                MiniDumpWithProcessThreadData = 0x00000100,
                MiniDumpWithPrivateReadWriteMemory = 0x00000200,
                MiniDumpWithoutOptionalData = 0x00000400,
                MiniDumpWithFullMemoryInfo = 0x00000800,
                MiniDumpWithThreadInfo = 0x00001000,
                MiniDumpWithCodeSegs = 0x00002000
            }

            [DllImport( "dbghelp.dll" )]
            static extern bool MiniDumpWriteDump (
                IntPtr hProcess,
                Int32 ProcessId,
                IntPtr hFile,
                MINIDUMP_TYPE DumpType,
                IntPtr ExceptionParam,
                IntPtr UserStreamParam,
                IntPtr CallackParam );

            public static void MiniDumpToFile ( String fileToDump )
            {
                FileStream fsToDump = null;
                if ( File.Exists( fileToDump ) )
                    fsToDump = File.Open( fileToDump, FileMode.Append );
                else
                    fsToDump = File.Create( fileToDump );
                Process thisProcess = Process.GetCurrentProcess( );
                MiniDumpWriteDump( thisProcess.Handle, thisProcess.Id,
                    fsToDump.SafeFileHandle.DangerousGetHandle( ), MINIDUMP_TYPE.MiniDumpNormal,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero );
                fsToDump.Close( );
            }
        }
}
