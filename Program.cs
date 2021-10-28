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

            /*
            HistoryLoader hl = new HistoryLoader();
            hl.Load(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2021\Data\index.dat");
            var serie = hl.club_comp.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == "French Ligue 2");
            foreach (var club in hl.club)
            {
                if (club.Division == serie.ID)
                {
                    Console.WriteLine("{0} ----- {1}  ({2})", MiscFunctions.GetTextFromBytes(club.Name), MiscFunctions.GetTextFromBytes(club.ShortName), club.Reputation);
                }
            }*/

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
