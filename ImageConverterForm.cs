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
    public partial class ImageConverterForm : Form
    {
        public ImageConverterForm()
        {
            InitializeComponent();
            checkBoxCrop_CheckedChanged(null, null);
        }

        private void buttonInputSelectFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.rgn/*.jpg/*.bmp/*.png)|*.jpg;*.bmp;*.rgn;*.png|All files (*.*)|*.*";
            ofd.Title = "Select an input image file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxInput.Text = ofd.FileName;
            }
        }

        private void buttonInputSelectFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder of images for processing...";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxInput.Text = fbd.SelectedPath;
            }
        }

        private void buttonOutputSelectFile_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog();
            ofd.Filter = "RGN Files (*.rgn)|*.rgn|All files (*.*)|*.*";
            ofd.Title = "Select an output image file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxOutput.Text = ofd.FileName;
            }
        }

        private void buttonOutputSelectFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder to output images to...";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxOutput.Text = fbd.SelectedPath;
            }
        }

        private void checkBoxOnlyProcessIfSize_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIfImageWidth.Enabled = textBoxIfImageHeight.Enabled = checkBoxOnlyProcessIfSize.Checked;
        }

        private void checkBoxResizeImagesTo_CheckedChanged(object sender, EventArgs e)
        {
            textBoxResizeImageWidth.Enabled = textBoxResizeImageHeight.Enabled = checkBoxResizeImagesTo.Checked;
        }

        private void checkBoxCrop_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLeft.Enabled = textBoxTop.Enabled = textBoxRight.Enabled = textBoxBottom.Enabled = checkBoxCrop.Checked;
        }


        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                var pf = new PictureConvertProgressForm();

                // Input
                List<string> inputFiles = new List<string>();
                FileAttributes attr = File.GetAttributes(textBoxInput.Text);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.rgn"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.jpg"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.bmp"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.png"));
                }
                else
                    inputFiles.Add(textBoxInput.Text);

                // Output
                bool isDirectory = false;
                string outputPath = textBoxOutput.Text;
                if (File.Exists(textBoxOutput.Text) && (File.GetAttributes(textBoxOutput.Text) & FileAttributes.Directory) == FileAttributes.Directory)
                    isDirectory = true;

                int checkSizeWidth = -1, checkSizeHeight = -1;
                if (checkBoxOnlyProcessIfSize.Checked)
                {
                    checkSizeWidth = int.Parse(textBoxIfImageWidth.Text);
                    checkSizeHeight = int.Parse(textBoxIfImageHeight.Text);
                }

                int newWidth = -1, newHeight = -1;
                if (checkBoxResizeImagesTo.Checked)
                {
                    newWidth = int.Parse(textBoxResizeImageWidth.Text);
                    newHeight = int.Parse(textBoxResizeImageHeight.Text);
                }

                int cropLeft = -1, cropTop = -1, cropRight = -1, cropBottom = -1;
                if (checkBoxCrop.Checked)
                {
                    cropLeft = int.Parse(textBoxLeft.Text);
                    cropTop = int.Parse(textBoxTop.Text);
                    cropRight = int.Parse(textBoxRight.Text);
                    cropBottom = int.Parse(textBoxBottom.Text);
                }

                pf.OnLoadAction = () =>
                {
                    new Thread(() =>
                    {
                        try
                        {
                            int converting = 1;
                            Thread.CurrentThread.IsBackground = true;
                            foreach (var picFile in inputFiles)
                            {
                                pf.SetProgressText(string.Format("Converting {0}/{1} ({2})", converting++, inputFiles.Count(), Path.GetFileName(picFile)));
                                pf.SetProgressPercent((int)(((double)(converting - 1) / ((double)inputFiles.Count())) * 100.0));

                                if (checkSizeWidth != -1 || checkSizeHeight != -1)
                                {
                                    int Width, Height;
                                    RGNConverter.GetImageSize(picFile, out Width, out Height);
                                    if (Width != checkSizeWidth || Height != checkSizeHeight)
                                        continue;
                                }

                                var outputTo = outputPath;
                                if (isDirectory)
                                    outputTo = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(picFile) + ".rgn");

                                if (Path.GetExtension(picFile).ToLower() == ".rgn")
                                {
                                    RGNConverter.RGN2RGN(picFile, outputTo, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom);
                                }
                                else
                                {
                                    RGNConverter.BMP2RGN(picFile, outputTo, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionMsgBox.Show(ex);
                        }
                        pf.CloseForm();
                    }).Start();
                };

                pf.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
        }
    }
}
