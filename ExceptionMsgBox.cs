using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public class ExceptionMsgBox
    {
        public static void Show(Exception ex)
        {
            var exceptionText = ex.Message + "\r\n\r\n";
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            var frames = trace.GetFrames();
            if (frames != null)
            {
                foreach (var F in trace.GetFrames())
                {
                    exceptionText += ("  " + System.IO.Path.GetFileName(F.GetFileName()) + " (" + F.GetFileLineNumber() + ")" + " - " + F.GetMethod().ToString() + "\r\n");
                }
            }
            MessageBox.Show(exceptionText, "!! Exception !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
