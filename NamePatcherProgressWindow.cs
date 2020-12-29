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
    public partial class NamePatcherProgressWindow : Form
    {
        public NamePatcherProgressWindow()
        {
            InitializeComponent();
        }

        public void CloseForm()
        {
            this.Invoke((MethodInvoker)delegate {
                this.Close();
            });
        }

        public void SetProgressPercent(int percent)
        {
            progressBar.Invoke((MethodInvoker)delegate {
                progressBar.Value = percent;
            });
        }
    }
}
