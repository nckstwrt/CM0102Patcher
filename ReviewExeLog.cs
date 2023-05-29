using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class ReviewExeLog : Form
    {
        public ReviewExeLog()
        {
            InitializeComponent();
        }

        public ReviewExeLog(string exeFile)
        {
            InitializeComponent();

            ReviewExeLog_ResizeEnd(null, null);

            if (!string.IsNullOrEmpty(exeFile))
            {
                var lines = Logger.ReadStrings(exeFile);
                foreach (var line in lines)
                {
                    textBoxLog.Text += line + "\r\n";
                }
                textBoxLog.Select(0, 0);
            }
        }

        private void ReviewExeLog_ResizeEnd(object sender, EventArgs e)
        {
            textBoxLog.Width = Width - 45;
            textBoxLog.Height = Height - 100;
            buttonCopyToClipboard.Left = Width - (buttonCopyToClipboard.Width + 30);
            buttonCopyToClipboard.Top = Height - 75;
            buttonClose.Left = (Width / 2) - (buttonClose.Width / 2);
            buttonClose.Top = Height - 75;
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(textBoxLog.Text);
            MessageBox.Show("Log has been copied to the clipboard", "Review EXE Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
