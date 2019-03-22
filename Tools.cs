using CM0102Patcher.Scouter;
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
    public partial class Tools : Form
    {
        string exeFile;

        public Tools(string exeFile)
        {
            this.exeFile = exeFile;
            InitializeComponent();
        }

        private void buttonApplyPatchfile_Click(object sender, EventArgs e)
        {
          //  try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "CM0102.exe Patch|*.patch|Text Files|*.txt|All files (*.*)|*.*";
                ofd.Title = "Select a CM0102 .patch file";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Patcher patcher = new Patcher();
                    var patch = patcher.LoadPatchFile(ofd.FileName);
                    patcher.ApplyPatch(exeFile, patch);
                    MessageBox.Show("Patch applied successfully!", "Patch Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            /*catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }*/
        }

        private void buttonOffsetCalculator_Click(object sender, EventArgs e)
        {
            var oc = new OffsetCalculator();
            oc.ShowDialog();
        }

        private bool EECHack(string nationFile)
        {
            using (var file = File.Open(nationFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    using (var br = new BinaryReader(file))
                    {
                        while (true)
                        {
                            file.Seek(0x7f, SeekOrigin.Current);
                            var eec = br.ReadByte();
                            if (eec > 3)
                            {
                                MessageBox.Show("Weird EEC byte read! Exiting...");
                                return false;
                            }
                            if (eec == 0x01 || eec == 0x00)
                            {
                                file.Seek(-1, SeekOrigin.Current);
                                bw.Write((byte)2);
                            }
                            file.Seek(-(0x7f + 1), SeekOrigin.Current);
                            file.Seek(0x122, SeekOrigin.Current);
                            if (file.Position + 0x122 >= file.Length)
                                break;
                        }
                    }
                }
            }
            return true;
        }

        private void buttonEECPatcher_Click(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "CM0102 nation.dat file|nation.dat|All files (*.*)|*.*";
                ofd.Title = "Select a CM0102 nation.dat file";
                try
                {
                    var path = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                    if (!string.IsNullOrEmpty(path))
                        ofd.InitialDirectory = Path.Combine(path, "Data");
                }
                catch { }
                        
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var yesNo = MessageBox.Show("This will make all countries EEC members removing the need for work permits. Continue?", "EEC Patcher", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (yesNo == DialogResult.Yes)
                    {
                        if (EECHack(ofd.FileName))
                            MessageBox.Show("EEC Hack applied successfully!", "EEC Hack Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
        }

        private void buttonRefereePatcher_Click(object sender, EventArgs e)
        {
            var rpf = new RefereePatcherForm();
            rpf.ShowDialog();
        }

        private void buttonSaveScouter_Click(object sender, EventArgs e)
        {
            var sg = new ScoutGrid();
            sg.ShowDialog();
        }

        private void buttonRGNImageConverter_Click(object sender, EventArgs e)
        {
            var imgConverter = new ImageConverterForm();
            imgConverter.ShowDialog();
        }
    }
}
