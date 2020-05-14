using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class RefereePatcherForm : Form
    {
        public RefereePatcherForm()
        {
            InitializeComponent();

            try
            {
                if (!string.IsNullOrEmpty(RegString.GetRegString()))
                {
                    var path = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                    if (!string.IsNullOrEmpty(path))
                    {
                        var dataPath = Path.Combine(path, "Data");
                        labelFilename.Text = Path.Combine(dataPath, "officials.dat");
                    }
                }
            }
            catch { }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "CM0102 officials.dat file|officials.dat|All files (*.*)|*.*";
                ofd.Title = "Select a CM0102 officials.dat file";
                try
                {
                    if (!string.IsNullOrEmpty(RegString.GetRegString()))
                    {
                        var path = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                        if (!string.IsNullOrEmpty(path))
                            ofd.InitialDirectory = Path.Combine(path, "Data");
                    }
                }
                catch { }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    labelFilename.Text = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(labelFilename.Text))
                {
                    MessageBox.Show("Please select an officals.dat to modify", "Select File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBoxDiscplineFixed.Checked && (((int)numericModifier.Value) > 20 || ((int)numericModifier.Value) < 1))
                {
                    MessageBox.Show("Set Discipine Value Between 1-20!", "Discipline Only Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var rp = new RefereePatcher();
                rp.PatchOfficialsFile(labelFilename.Text, (int)numericModifier.Value, checkBoxDiscplineFixed.Checked);
                MessageBox.Show("Officals.dat Patched!", "Referee Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
        }

        private void checkBoxDiscplineFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDiscplineFixed.Checked)
            {
                label3.Text = "Set All Refs Discipline To:";
                if (numericModifier.Value > 20)
                    numericModifier.Value = 5;
            }
            else
                label3.Text = "CA/PA/Discipline Percentage Modifier:";
        }
    }
}
