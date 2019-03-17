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

        public PictureConvertProgressForm(string picturesDir)
        {
            InitializeComponent();

            new Thread(() =>
            {
                int converting = 1;
                Thread.CurrentThread.IsBackground = true;
                var picFiles = Directory.GetFiles(picturesDir, "*.rgn");
                foreach (var picFile in picFiles)
                {
                    SetProgressText(string.Format("Converting {0}/{1} ({2})", converting++, picFiles.Length, Path.GetFileName(picFile)));
                    SetProgressPercent((int)(((double)(converting-1) / ((double)picFiles.Length)) * 100.0));
                    int Width, Height;
                    RGNConverter.GetRGNSize(picFile, out Width, out Height);
                    if (Width == 800 && Height == 600)
                    {
                        RGNConverter.RGN2RGN(picFile, picFile + ".tmp", 1280, 800);
                        File.Delete(picFile);
                        File.Move(picFile + ".tmp", picFile);
                    }
                }
                CloseForm();
            }).Start();
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
    }
}
