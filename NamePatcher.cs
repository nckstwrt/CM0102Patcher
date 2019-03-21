using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM0102Patcher
{
    public class NamePatcher
    {
        string exeFile;
        string dataDir;
       
        public NamePatcher(string exeFile, string dataDir)
        {
            this.exeFile = exeFile;
            this.dataDir = dataDir;
        }

        public void RunPatch()
        {
            PatchHoland();
        }

        void PatchHoland()
        {

        }

        void PatchComps(string exeFile, string dataDir)
        {
        }
    }
}
