using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;

namespace CM0102Patcher
{
    public class RestorePoint
    {
        private static void CreateDirectory(string dirName)
        {
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);
        }

        private static void CopyFile(string srcFile, string dstFile)
        {
            if (File.Exists(dstFile))
                File.SetAttributes(dstFile, FileAttributes.Normal);
            File.Copy(srcFile, dstFile, true);
        }

        public static void CopyTo(Stream input, Stream output)
        {
            byte[] buffer = new byte[32 * 1024];
            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

        private static void CompressFile(string srcFile, string dstFile, bool decompress = false)
        {
            if (File.Exists(dstFile))
                File.SetAttributes(dstFile, FileAttributes.Normal);
            using (var fsIn = File.Open(srcFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var fsOut = File.Open(dstFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var gz = new GZipStream(decompress ? fsIn : fsOut, decompress ? CompressionMode.Decompress : CompressionMode.Compress))
                    {
                        if (decompress)
                            CopyTo(gz, fsOut);
                        else
                            CopyTo(fsIn, gz);
                    }
                }
            }
        }

        public static void Save(string exeFile)
        {
            try
            {
                var dir = Path.GetDirectoryName(exeFile);
                var dataDir = Path.Combine(dir, "Data");
                var picturesDir = Path.Combine(dir, "Pictures");
                var restorePointDir = Path.Combine(dir, "RestorePoint");
                var restorePointExeDir = Path.Combine(restorePointDir, "exe");
                var restorePointDataDir = Path.Combine(restorePointDir, "Data");
                var restorePointPicturesDir = Path.Combine(restorePointDir, "Pictures");
                if (Directory.Exists(restorePointDir))
                {
                    if (MessageBox.Show("Restore point already exists. Do you wish to overwrite?", "Restore point", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                }
                else
                    CreateDirectory(restorePointDir);

                CreateDirectory(restorePointExeDir);
                CreateDirectory(restorePointDataDir);

                var pf = new PictureConvertProgressForm("Restore Point");
                pf.OnLoadAction = () =>
                {
                    new Thread(() =>
                    {
                        try
                        {
                            int copying = 1;
                            Thread.CurrentThread.IsBackground = true;

                            // Copy CM0102.exe
                            CompressFile(exeFile, Path.Combine(restorePointExeDir, "cm0102.exe" + ".gz"));

                            // Copy Data
                            var dataFiles = Directory.GetFiles(dataDir, "*.*");
                            foreach (var dataFile in dataFiles)
                            {
                                pf.SetProgressText(string.Format("Copying {0}/{1} ({2})", copying++, dataFiles.Length, Path.GetFileName(dataFile)));
                                pf.SetProgressPercent((int)(((double)(copying - 1) / ((double)dataFiles.Length)) * 100.0));
                                CompressFile(dataFile, Path.Combine(restorePointDataDir, Path.GetFileName(dataFile) + ".gz"));
                            }

                            // Copy Pictures
                            if (Directory.Exists(picturesDir))
                            {
                                CreateDirectory(restorePointPicturesDir);
                                var pictureFiles = Directory.GetFiles(picturesDir, "*.*");
                                copying = 1;
                                foreach (var pictureFile in pictureFiles)
                                {
                                    pf.SetProgressText(string.Format("Copying {0}/{1} ({2})", copying++, pictureFiles.Length, Path.GetFileName(pictureFile)));
                                    pf.SetProgressPercent((int)(((double)(copying - 1) / ((double)pictureFiles.Length)) * 100.0));
                                    CompressFile(pictureFile, Path.Combine(restorePointPicturesDir, Path.GetFileName(pictureFile) + ".gz"));
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

        public static void Restore(string exeFile)
        {
            try
            {

                var dir = Path.GetDirectoryName(exeFile);
                var dataDir = Path.Combine(dir, "Data");
                var picturesDir = Path.Combine(dir, "Pictures");
                var restorePointDir = Path.Combine(dir, "RestorePoint");
                var restorePointExeDir = Path.Combine(restorePointDir, "exe");
                var restorePointDataDir = Path.Combine(restorePointDir, "Data");
                var restorePointPicturesDir = Path.Combine(restorePointDir, "Pictures");
                if (Directory.Exists(restorePointDir))
                {
                    if (MessageBox.Show("Are sure you want to overwrite your current cm0102.exe, data and pictures from the restore point?", "Restore point", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                }
                else
                {
                    MessageBox.Show("Restore point not found. Unable to restore.", "Restore point", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var pf = new PictureConvertProgressForm("Restore Point");
                pf.OnLoadAction = () =>
                {
                    new Thread(() =>
                    {
                        try
                        {
                            int copying = 1;
                            Thread.CurrentThread.IsBackground = true;

                            // Copy CM0102.exe
                            CompressFile(Path.Combine(restorePointExeDir, "cm0102.exe.gz"), exeFile, true);

                            // Copy Data
                            var dataFiles = Directory.GetFiles(restorePointDataDir, "*.*");
                            foreach (var dataFile in dataFiles)
                            {
                                pf.SetProgressText(string.Format("Copying {0}/{1} ({2})", copying++, dataFiles.Length, Path.GetFileName(dataFile)));
                                pf.SetProgressPercent((int)(((double)(copying - 1) / ((double)dataFiles.Length)) * 100.0));
                                CompressFile(dataFile, Path.Combine(dataDir, Path.GetFileName(dataFile).Replace(".gz", "")), true);
                            }

                            // Copy Pictures
                            if (Directory.Exists(restorePointPicturesDir))
                            {
                                var pictureFiles = Directory.GetFiles(restorePointPicturesDir, "*.*");
                                copying = 1;
                                foreach (var pictureFile in pictureFiles)
                                {
                                    pf.SetProgressText(string.Format("Copying {0}/{1} ({2})", copying++, pictureFiles.Length, Path.GetFileName(pictureFile)));
                                    pf.SetProgressPercent((int)(((double)(copying - 1) / ((double)pictureFiles.Length)) * 100.0));
                                    CompressFile(pictureFile, Path.Combine(picturesDir, Path.GetFileName(pictureFile).Replace(".gz", "")), true);
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
