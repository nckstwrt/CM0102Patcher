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
    public partial class UpdatingForm : Form
    {
        public UpdatingForm()
        {
            InitializeComponent();
        }

        private void UpdatingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        public void SetUpdateText(string text)
        {
            if (this.Visible)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    labelProcessing.Text = "Currently Processing: " + text;
                    labelProcessing.Update();
                    Update();
                    Application.DoEvents();
                });
            }
        }
    }
}
