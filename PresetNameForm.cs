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
    public partial class PresetNameForm : Form
    {
        public string PresetName = "";

        public PresetNameForm()
        {
            InitializeComponent();
            textBoxPresetName.Text = PresetName;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxPresetName.Text.Length == 0)
            {
                MessageBox.Show(this, "Please give a name for the preset", "Preset Name Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PresetName = textBoxPresetName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxPresetName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSave_Click(this, new EventArgs());
            }
        }
    }
}
