using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.IO.Compression;

namespace CM0102Patcher
{
    public partial class MiscPatches : Form
    {
        string exeFile;
        List<ZipStorer.ZipFileEntry> patchFiles;
        List<ZipStorer.ZipFileEntry> txtFiles;

        public MiscPatches(string exeFile)
        {
            this.exeFile = exeFile;
            InitializeComponent();

            using (var zs = OpenZip())
            {
                patchFiles = zs.ReadCentralDir().FindAll(x => x.FilenameInZip.Contains(".patch"));
                txtFiles = zs.ReadCentralDir().FindAll(x => x.FilenameInZip.Contains(".txt"));
            }

            foreach (var patch in patchFiles)
                checkedListBoxPatches.Items.Add(patch);

            Detect();
        }

        void Detect()
        {
            var patcher = new Patcher();
            using (var zs = OpenZip())
            {
                for (int i = 0; i < checkedListBoxPatches.Items.Count; i++)
                {
                    using (var ms = new MemoryStream())
                    {
                        var patch = (ZipStorer.ZipFileEntry)checkedListBoxPatches.Items[i];
                        zs.ExtractFile(patch, ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        var hexPatch = patcher.LoadPatchFile(ms);
                        if (patcher.DetectPatch(exeFile, hexPatch))
                        {
                            checkedListBoxPatches.SetItemChecked(i, true);
                            checkedListBoxPatches.SetItemCheckState(i, CheckState.Indeterminate);
                        }
                    }
                }
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (checkedListBoxPatches.CheckedItems.Count == 0)
            {
                MessageBox.Show("No patches selected. Closing.", "Misc Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            var yesNo = MessageBox.Show("Are you sure you want to apply these to your exe?\r\nYou could break your cm0102.exe - are you really sure?", "Misc Patches", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (yesNo == DialogResult.Yes)
            {
                for (int i = 0; i < checkedListBoxPatches.Items.Count; i++)
                {
                    if (checkedListBoxPatches.GetItemChecked(i) && checkedListBoxPatches.GetItemCheckState(i) != CheckState.Indeterminate)
                    {
                        var patch = (ZipStorer.ZipFileEntry)checkedListBoxPatches.Items[i];
                        using (var zs = OpenZip())
                        {
                            using (var ms = new MemoryStream())
                            {
                                zs.ExtractFile(patch, ms);
                                ms.Seek(0, SeekOrigin.Begin);

                                Patcher patcher = new Patcher();
                                var hexPatch = patcher.LoadPatchFile(ms);
                                patcher.ApplyPatch(exeFile, hexPatch);
                            }
                        }
                    }
                }

                MessageBox.Show("Patches Applied!", "Patches Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ZipStorer OpenZip()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("MiscPatches.zip"));
            return ZipStorer.Open(assembly.GetManifestResourceStream(resourceName), FileAccess.Read);
        }

        private void checkedListBoxPatches_SelectedValueChanged(object sender, EventArgs e)
        {
            var patch = (ZipStorer.ZipFileEntry)checkedListBoxPatches.SelectedItem;
            var txtFileName = patch.FilenameInZip.Replace(".patch", ".txt");
            if (txtFiles.Exists(x => x.FilenameInZip == txtFileName))
            {
                var txtFile = txtFiles.First(x => x.FilenameInZip == txtFileName);
                using (var zs = OpenZip())
                {
                    using (var ms = new MemoryStream())
                    {
                        zs.ExtractFile(txtFile, ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        using (var tr = new StreamReader(ms))
                        {
                            var text = tr.ReadToEnd();
                            // Help support Unix files that don't have MS \r
                            text = text.Replace("\r\n", "\n");
                            text = text.Replace("\n", "\r\n");
                            textBoxDescription.Text = text;
                        }
                    }
                }
            }
            else
                textBoxDescription.Text = "No Description.";
        }
    }
}
