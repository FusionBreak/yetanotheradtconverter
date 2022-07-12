using System.Collections.Generic;
using System.IO;
using YetAnotherAdtConverter.Files.BFA.Chunks;


namespace YetAnotherAdtConverter.Files.BFA
{
    [System.Diagnostics.DebuggerDisplay("{ADTfileInfo.Name}")]
    class OBJ0
    {
        FileInfo ADTfileInfo;
        int MCNKLength = 0;

        public MVER MVER;
        public MMDX MMDX;
        public MMID MMID;
        public MWMO MWMO;
        public MWID MWID;
        public MDDF MDDF;
        public MODF MODF;

        List<MCNK_OBJ0> MCNKs = new List<MCNK_OBJ0>();

        public OBJ0(Files.WOTLK.ADT wotlk)
        {
            ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0] + "_obj0.adt");

            MVER = new MVER(wotlk.MVER);
            MMDX = new MMDX(wotlk.MMDX);
            MMID = new MMID(wotlk.MMID);
            MWMO = new MWMO(wotlk.MWMO);
            MWID = new MWID(wotlk.MWID);
            MDDF = new MDDF(wotlk.MDDF);
            MODF = new MODF(wotlk.MODF);

            foreach(Files.WOTLK.Chunks.MCNK x in wotlk.MCNKs)
            {
                MCNKs.Add(new MCNK_OBJ0(x));
                MCNKLength += MCNKs[MCNKs.Count - 1].GetBytes().Length;
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
                writer.Write(MMDX.GetBytes());
                writer.Write(MMID.GetBytes());
                writer.Write(MWMO.GetBytes());
                writer.Write(MWID.GetBytes());
                writer.Write(MDDF.GetBytes());
                writer.Write(MODF.GetBytes());


                foreach(MCNK_OBJ0 x in MCNKs)
                {
                    writer.Write(x.GetBytes());
                }
            }
        }
    }
}
