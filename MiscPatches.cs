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

        public MiscPatches(string exeFile)
        {
            this.exeFile = exeFile;
            InitializeComponent();

            using (var zs = OpenZip())
            {
                patchFiles = zs.ReadCentralDir().FindAll(x => x.FilenameInZip.Contains(".patch"));
            }

            foreach (var patch in patchFiles)
                checkedListBoxPatches.Items.Add(patch);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (checkedListBoxPatches.CheckedItems.Count == 0)
            {
                MessageBox.Show("No patches selected. Closing.", "Misc Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                return;
            }

            var yesNo = MessageBox.Show("Are you sure you want to apply these to your exe?\r\nYou will most likely break your cm0102.exe - are you really sure?\r\nLike, really sure?", "Thar Be Dragons", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (yesNo == DialogResult.Yes)
            {
                foreach (var checkedItem in checkedListBoxPatches.CheckedItems)
                {
                    var patch = (ZipStorer.ZipFileEntry)checkedItem;
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

                MessageBox.Show("Patches Applied!\r\n\r\nYou mad fool :)", "Patches Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ZipStorer OpenZip()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("MiscPatches.zip"));
            return ZipStorer.Open(assembly.GetManifestResourceStream(resourceName), FileAccess.Read);
        }
    }
}
