using System.IO;
using YetAnotherAdtConverter.Files.BFA.Chunks;

namespace YetAnotherAdtConverter.Files.BFA
{
    [System.Diagnostics.DebuggerDisplay("{ADTfileInfo.Name}")]
    class OBJ1
    {
        FileInfo ADTfileInfo;

        public MVER MVER;
        public MLFD MLFD;
        public MMDX MMDX;
        public MMID MMID;
        public MWMO MWMO;
        public MWID MWID;
        public MLDD MLDD;
        public MLDX MLDX;
        public MLMD MLMD;
        public MLMX MLMX;

        public OBJ1(Files.WOTLK.ADT wotlk)
        {
            ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0] + "_obj1.adt");

            MVER = new MVER(wotlk.MVER);
            MLFD = new MLFD(wotlk.MDDF, wotlk.MODF); //I'm not sure the chunk is really needed.
            MMDX = new MMDX(wotlk.MMDX);
            MMID = new MMID(wotlk.MMID);
            MWMO = new MWMO(wotlk.MWMO);
            MWID = new MWID(wotlk.MWID);
            MLDD = new MLDD(wotlk.MDDF);
            MLDX = new MLDX(wotlk.MDDF);
            MLMD = new MLMD(wotlk.MODF);
            MLMX = new MLMX(wotlk.MODF);
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

                if(MLFD != null)
                    writer.Write(MLFD.GetBytes());

                writer.Write(MMDX.GetBytes());
                writer.Write(MMID.GetBytes());
                writer.Write(MWMO.GetBytes());
                writer.Write(MWID.GetBytes());
                writer.Write(MLDD.GetBytes());
                writer.Write(MLDX.GetBytes());
                writer.Write(MLMD.GetBytes());
                writer.Write(MLMX.GetBytes());
            }
        }
    }
}
