using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using YetAnotherAdtConverter.Files.BFA.Chunks;

namespace YetAnotherAdtConverter.Files.BFA
{
    [System.Diagnostics.DebuggerDisplay("{ADTfileInfo.Name}")]
    class TEX0
    {
        FileInfo ADTfileInfo;
        int MCNKLength = 0;

        public MVER MVER;
        public MAMP MAMP;
        public MTEX MTEX;

        List<MCNK_TEX0> MCNKs = new List<MCNK_TEX0>();

        public TEX0(Files.WOTLK.ADT wotlk)
        {
            ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0] + "_tex0.adt");
            Logger.log(ADTfileInfo.Name, Logger.Type.CONVERT, "<- " + wotlk.ADTfileInfo.Name);

            MVER = new MVER(wotlk.MVER);
            MAMP = new MAMP();
            MTEX = new MTEX(wotlk.MTEX);

            foreach (Files.WOTLK.Chunks.MCNK x in wotlk.MCNKs)
            {
                MCNKs.Add(new MCNK_TEX0(x));
                //MCNKLength += MCNKs[MCNKs.Count - 1].GetBytes().Length;
            }
            Logger.log("MCNK[]", Logger.Type.LEVEL1);

        }

        public void WriteFile(DirectoryInfo directory)
        {
            Logger.log(ADTfileInfo.Name, Logger.Type.WRITE, directory.FullName);

            if (!directory.Exists)
            {
                directory.Create();
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(directory.FullName + "\\" + ADTfileInfo.Name, FileMode.Create)))
            {
                writer.Write(MVER.GetBytes());
                writer.Write(MAMP.GetBytes());
                writer.Write(MTEX.GetBytes());

                foreach (MCNK_TEX0 x in MCNKs)
                {
                    writer.Write(x.GetBytes());
                }


                Logger.log(DateTime.Now.ToString(), Logger.Type.LEVEL1);
            }
        }
    }
}
