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
            //CM9798.Test();
            /*
            CM2 cm2 = new CM2();
            cm2.ReadData();
            */
            //CM9798.SavedPlayerCount(@"C:\ChampMan\cm9798\Fresh\Data\CM9798\PLDATA1.S16");
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
