using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CM0102Patcher
{
    static public class Logger
    {
        public static void Log(string exeFile, string format, params string[] args)
        {
            try
            {
                var textString = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + ": "  + string.Format(format, args) + "\r\n";
                using (var file = File.Open(exeFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (var sw = new StreamWriter(file))
                {
                    file.Seek(0, SeekOrigin.End);
                    sw.Write(textString);
                }
            }
            catch
            {
            }
        }

        public static List<string> ReadStrings(string exeFile)
        {
            List<string> strings = new List<string>();
            try 
            { 
                using (var file = File.Open(exeFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var sr = new StreamReader(file))
                {
                    if (file.Length >= 9289728)
                    {
                        file.Seek(9289728, SeekOrigin.Begin);  // End of Expanded Exe
                        while (true)
                        {
                            var line = sr.ReadLine();
                            if (string.IsNullOrEmpty(line))
                                break;
                            strings.Add(line);
                        }
                    }
                }
            }
            catch
            {
            }
            return strings;
        }
    }
}
