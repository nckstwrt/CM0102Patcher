using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CM0102Patcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            CM2 cm2 = new CM2();
            cm2.ReadData();
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PatcherForm());
        }

        public static bool RunningInMono()
        {
            return (Type.GetType("Mono.Runtime") != null);
        }
    }
}
