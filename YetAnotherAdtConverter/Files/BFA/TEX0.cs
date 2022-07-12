using System.Collections.Generic;
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

            MVER = new MVER(wotlk.MVER);
            MAMP = new MAMP();
            MTEX = new MTEX(wotlk.MTEX);

            foreach(Files.WOTLK.Chunks.MCNK x in wotlk.MCNKs)
            {
                MCNKs.Add(new MCNK_TEX0(x));
                //MCNKLength += MCNKs[MCNKs.Count - 1].GetBytes().Length;
            }
        }

        public void WriteFile(DirectoryInfo directory)
        {
            if(!directory.Exists)
            {
                directory.Create();
            }

            using(BinaryWriter writer = new BinaryWriter(File.Open(directory.FullName + "\\" + ADTfileInfo.Name, FileMode.Create)))
            {
                writer.Write(MVER.GetBytes());
                writer.Write(MAMP.GetBytes());
                writer.Write(MTEX.GetBytes());

                foreach(MCNK_TEX0 x in MCNKs)
                {
                    writer.Write(x.GetBytes());
                }
            }
        }
    }
}
