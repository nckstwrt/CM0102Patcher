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
    public partial class PlayerTransferScratchPadForm : Form
    {
        public string TransferText = "";

        public PlayerTransferScratchPadForm()
        {
            InitializeComponent();
            textBox.MaxLength = 0;
        }

        public void SetText(string text)
        {
            textBox.Text = text;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            TransferText = textBox.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
