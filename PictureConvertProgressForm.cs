using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class PictureConvertProgressForm : Form
    {
        bool stopClose = true;

        public PictureConvertProgressForm(string windowTitle = null)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(windowTitle))
                this.Text = windowTitle;
        }

        public void CloseForm()
        {
            this.Invoke((MethodInvoker)delegate {
                stopClose = false;
                this.Close();
            });
        }

        public void SetProgressPercent(int percent)
        {
            progressBar.Invoke((MethodInvoker)delegate {
                progressBar.Value = percent;
            });
        }

        public void SetProgressText(string text)
        {
            labelProgress.Invoke((MethodInvoker)delegate {
                labelProgress.Text = text;
            });
        }

        private void PictureConvertProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = stopClose;
        }

        private void PictureConvertProgressForm_Load(object sender, EventArgs e)
        {
            if (OnLoadAction != null)
                OnLoadAction();
        }

        public Action OnLoadAction;
    }
}
