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

            var latvia = hl.nation.First(x => x.Name.ReadString() == "Bolivia");
            foreach (var club in hl.club)
            {
                if (club.Nation == latvia.ID)
                {
                    Console.WriteLine("{0} - {1}", club.Name.ReadString(), club.ShortName.ReadString());
                }
            }*/

                /*
                var serie = hl.club_comp.FirstOrDefault(x => MiscFunctions.GetTextFromBytes(x.Name) == "French Ligue 2");
                int zerorep = 0;
                //foreach (var club in hl.club)
                List<int> removeClubs = new List<int>();
                for (int i = 0; i < hl.club.Count; i++)
                {
                    if (hl.club[i].Division == -1 && hl.club[i].Reputation > 0 && hl.club[i].Reputation <= 500 && hl.club[i].HasLinkedClub == 0)
                        removeClubs.Add(i);
                    /*
                    if (hl.club[i].Reputation <= 1000)
                    {
                        var temp = hl.club[i];
                        temp.Reputation = 1;
                        hl.club[i] = temp;
                    }*/

                //if (club.Reputation > 1000 && club.Division == -1)
                //  Console.WriteLine("Hello");
                /*6
                if (club.Division == serie.ID)
                {
                    Console.WriteLine("{0} ----- {1}  ({2})", MiscFunctions.GetTextFromBytes(club.Name), MiscFunctions.GetTextFromBytes(club.ShortName), club.Reputation);
                }*/
                /*}
                removeClubs.Sort();
                removeClubs.Reverse();
                foreach (var remove in removeClubs)
                    hl.club.RemoveAt(remove);
                */
                //  hl.Save(@"C:\ChampMan\Championship Manager 0102\TestQuick\Oct2021\Data\index.dat", true);

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
