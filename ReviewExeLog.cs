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
            textBoxLog.Height = Height - 70;
        }
    }
}
