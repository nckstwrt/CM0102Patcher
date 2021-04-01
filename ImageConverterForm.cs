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
            ofd.Filter = "Image Files (*.rgn;*.jpg;*.bmp;*.png;*.pcx)|*.jpg;*.hsr;*.bmp;*.rgn;*.png;*.pcx|All files (*.*)|*.*";
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
            var formats = new string[] { "RGN", "JPG", "BMP", "PNG", "HSR", "PCX" };
            var Filter = "";
            foreach (var format in formats)
                Filter += string.Format("{0} Files (*.{1})|*.{1}|", format, format.ToLower());
            Filter += "All files (*.*)|*.*";
            ofd.Filter = Filter;

            if (radioButtonRGN.Checked)
                ofd.FilterIndex = 1;
            if (radioButtonJPG.Checked)
                ofd.FilterIndex = 2;
            if (radioButtonBMP.Checked)
                ofd.FilterIndex = 3;
            if (radioButtonPNG.Checked)
                ofd.FilterIndex = 4;
            if (radioButtonPCX.Checked)
                ofd.FilterIndex = 6;

            ofd.Title = "Select an output image file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxOutput.Text = ofd.FileName;

                switch (Path.GetExtension(ofd.FileName).ToLower())
                {
                    case ".hsr":
                    case ".rgn":
                        radioButtonRGN.Checked = true;
                        break;
                    case ".jpg":
                        radioButtonJPG.Checked = true;
                        break;
                    case ".bmp":
                        radioButtonBMP.Checked = true;
                        break;
                    case ".png":
                        radioButtonPNG.Checked = true;
                        break;
                    case ".pcx":
                        radioButtonPCX.Checked = true;
                        break;
                }
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
            checkBoxAutoCrop.Checked = false;
            checkBoxAutoCrop.Enabled = textBoxLeft.Enabled = textBoxTop.Enabled = textBoxRight.Enabled = textBoxBottom.Enabled = checkBoxCrop.Checked;
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
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.hsr"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.rgn"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.jpg"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.bmp"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.png"));
                    inputFiles.AddRange(Directory.GetFiles(textBoxInput.Text, "*.pcx"));
                }
                else
                    inputFiles.Add(textBoxInput.Text);

                // Output
                bool isDirectory = false;
                string outputPath = textBoxOutput.Text;
                if (Directory.Exists(textBoxOutput.Text))
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

                float brightness = ((float)numericBrightness.Value) + 1.0f;

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
                                    if (RGNConverter.GetImageSize(picFile, out Width, out Height))
                                    {
                                        if (Width != checkSizeWidth || Height != checkSizeHeight)
                                            continue;
                                    }
                                    else
                                        continue;
                                }

                                var outputTo = outputPath;

                                // If directory we need set the appropriate extension
                                if (isDirectory)
                                {
                                    var extension = ".rgn";
                                    switch (GetSelectedOutputFormat())
                                    {
                                        case CMImageFormat.BMP:
                                            extension = ".bmp";
                                            break;
                                        case CMImageFormat.GIF:
                                            extension = ".gif";
                                            break;
                                        case CMImageFormat.JPG:
                                            extension = ".jpg";
                                            break;
                                        case CMImageFormat.PCX:
                                            extension = ".pcx";
                                            break;
                                        case CMImageFormat.PNG:
                                            extension = ".png";
                                            break;
                                        case CMImageFormat.RGN:
                                            extension = ".rgn";
                                            break;
                                    }
                                    outputTo = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(picFile) + extension);
                                }

                                // Check the input file is a CM0102 type file
                                if (Path.GetExtension(picFile).ToLower() == ".rgn" || Path.GetExtension(picFile).ToLower() == ".hsr" || Path.GetExtension(picFile).ToLower() == ".mbr")
                                {
                                    // Check whether we are outputting to a cm0102 file and if we aren't then output to BMP
                                    if (GetSelectedOutputFormat() == CMImageFormat.RGN)
                                        RGNConverter.RGN2RGN(picFile, outputTo, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness, checkBoxAutoCrop.Checked, GetSelectedOutputFormat());     // Output CM0102 RGN file to another CM0102 RGN file
                                    else
                                        RGNConverter.RGN2BMP(picFile, outputTo, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness, checkBoxAutoCrop.Checked, GetSelectedOutputFormat());     // Output CM0102 RGN file to a Bitmap type
                                }
                                else
                                {
                                    // Outputting a Bitmap to a RGN or Bitmap
                                    if (GetSelectedOutputFormat() == CMImageFormat.RGN)
                                        RGNConverter.BMP2RGN(picFile, outputTo, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness, checkBoxAutoCrop.Checked, GetSelectedOutputFormat());
                                    else
                                        RGNConverter.BMP2BMP(picFile, outputTo, newWidth, newHeight, cropLeft, cropTop, cropRight, cropBottom, brightness, checkBoxAutoCrop.Checked, GetSelectedOutputFormat());
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

        private CMImageFormat GetSelectedOutputFormat()
        {
            if (radioButtonRGN.Checked)
                return CMImageFormat.RGN;
            if (radioButtonBMP.Checked)
                return CMImageFormat.BMP;
            if (radioButtonPNG.Checked)
                return CMImageFormat.PNG;
            if (radioButtonJPG.Checked)
                return CMImageFormat.JPG;
            if (radioButtonBMP.Checked)
                return CMImageFormat.BMP;
            if (radioButtonPCX.Checked)
                return CMImageFormat.PCX;
            return CMImageFormat.RGN;
        }

        private void checkBoxAutoCrop_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLeft.Enabled = !checkBoxAutoCrop.Checked;
            textBoxRight.Enabled = !checkBoxAutoCrop.Checked;
            textBoxTop.Enabled = !checkBoxAutoCrop.Checked;
            textBoxBottom.Enabled = !checkBoxAutoCrop.Checked;
        }
    }
}
