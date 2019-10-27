using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class Stadium
    {
        // From John Locke's Flex
        /*
        private static int STADIUM_ID = 0;//4
        private static int NAME = 4;//51
        private static int CITY = 56;//4
        private static int CAPACITY = 60;//4
        private static int SEATING = 64;//4
        */
        private static int EXPANSION_CAPACITY = 68;//4
        /*
        private static int NEARBY_STADIUM = 72;//4
        private static int IS_COVERED = 76;//1
        private static int SOIL_HEATED = 77;//1
        */
        public static int LENGTH = 78;

        public static void RemoveExpansionLimits(string stadiumFile)
        {
            using (var file = File.Open(stadiumFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var bw = new BinaryWriter(file))
                {
                    using (var br = new BinaryReader(file))
                    {
                        var fileLength = file.Length;
                        file.Seek(EXPANSION_CAPACITY, SeekOrigin.Current);

                        while (true)
                        {
                            bw.Write((int)0);
                            if (file.Position + LENGTH > fileLength)
                                break;
                            file.Seek(LENGTH-4, SeekOrigin.Current);
                        }
                    }
                }
            }
        }
    }
}
