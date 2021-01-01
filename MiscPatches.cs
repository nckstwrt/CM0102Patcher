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
        HashSet<string> SelectedPatches = new HashSet<string>();

        public MiscPatches(string exeFile)
        {
            this.exeFile = exeFile;
            InitializeComponent();

            using (var zs = MiscFunctions.OpenZip("MiscPatches.zip"))
            {
                patchFiles = zs.ReadCentralDir().FindAll(x => x.FilenameInZip.Contains(".patch"));
                txtFiles = zs.ReadCentralDir().FindAll(x => x.FilenameInZip.Contains(".txt") || x.FilenameInZip.Contains(".info"));
            }

            RefreshWithFilter();
        }

        void RefreshWithFilter(string filter = null)
        {
            checkedListBoxPatches.Items.Clear();

            foreach (var patch in patchFiles)
            {
                if (string.IsNullOrEmpty(filter) || patch.ToString().ToLower().Contains(filter.ToLower()))
                {
                    checkedListBoxPatches.Items.Add(patch);
                    if (SelectedPatches.Contains(patch.ToString()))
                        checkedListBoxPatches.SetItemChecked(checkedListBoxPatches.Items.Count - 1, true);
                }
            }

            Detect();
        }

        void Detect()
        {
            if (!string.IsNullOrEmpty(exeFile))
            {
                var patcher = new Patcher();
                using (var zs = MiscFunctions.OpenZip("MiscPatches.zip"))
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
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(exeFile))
            {
                MessageBox.Show("No exe selected. Closing.", "Misc Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

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
                        using (var zs = MiscFunctions.OpenZip("MiscPatches.zip"))
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

        private void checkedListBoxPatches_SelectedValueChanged(object sender, EventArgs e)
        {
            if (checkedListBoxPatches.SelectedItem != null)
            {
                var patch = (ZipStorer.ZipFileEntry)checkedListBoxPatches.SelectedItem;
                var txtFileName = patch.FilenameInZip.Replace(".patch", ".txt");
                var infoFileName = patch.FilenameInZip.Replace(".patch", ".info");
                if (txtFiles.Exists(x => x.FilenameInZip == txtFileName) || txtFiles.Exists(x => x.FilenameInZip == infoFileName))
                {
                    var txtFile = txtFiles.First(x => (x.FilenameInZip == txtFileName || x.FilenameInZip == infoFileName));
                    using (var zs = MiscFunctions.OpenZip("MiscPatches.zip"))
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
            else
                textBoxDescription.Text = "No Description.";
        }

        private void checkedListBoxPatches_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (sender != null)
            {
                if (e.CurrentValue == CheckState.Indeterminate)
                    e.NewValue = CheckState.Indeterminate;
                else
                {
                    if (e.NewValue == CheckState.Checked)
                        SelectedPatches.Add(checkedListBoxPatches.Items[e.Index].ToString());
                    else
                        SelectedPatches.Remove(checkedListBoxPatches.Items[e.Index].ToString());
                }
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshWithFilter(textBoxFilter.Text);
        }
    }

    public sealed class ExpandedCheckedListBox : CheckedListBox
    {
        public ExpandedCheckedListBox()
        {
            ItemHeight = 18;
        }

        public override int ItemHeight { get; set; }
    }
}
