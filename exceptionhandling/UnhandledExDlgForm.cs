using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimplePlainNote
{
    public partial class UnhandledExDlgForm: Form
    {

        public UnhandledExDlgForm()
        {
            InitializeComponent();
        }

        private void UnhandledExDlgForm_Load(object sender, EventArgs e)
        {
            buttonNotSend.Focus();
            labelExceptionDate.Text = String.Format(labelExceptionDate.Text, DateTime.Now);
            linkLabelData.Left = labelLinkTitle.Right;
        }        
    }
}