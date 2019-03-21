using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using YetAnotherAdtConverter.Files.BFA.Chunks;


namespace YetAnotherAdtConverter.Files.BFA
{
    [System.Diagnostics.DebuggerDisplay("{ADTfileInfo.Name}")]
    class ADT
    {
        FileInfo ADTfileInfo;
        int MCNKLength = 0;

        public MVER MVER;
        public MHDR MHDR;        
        public MH2O MH2O;
        List<MCNK_ADT> MCNKs = new List<MCNK_ADT>();
        public MFBO MFBO;

        public ADT(Files.WOTLK.ADT wotlk)
        {
            ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0]+".adt");
            Logger.log(ADTfileInfo.Name, Logger.Type.CONVERT, "<- " + wotlk.ADTfileInfo.Name);


            MVER = new MVER(wotlk.MVER);
            MHDR = new MHDR(wotlk.MHDR);

            if (wotlk.MH2O != null)
                MH2O = new MH2O(wotlk.MH2O);

            foreach (Files.WOTLK.Chunks.MCNK x in wotlk.MCNKs)
            {
                MCNKs.Add(new MCNK_ADT(x));
                MCNKLength += MCNKs[MCNKs.Count - 1].GetBytes().Length;
            }
            Logger.log("MCNK[]", Logger.Type.LEVEL1);

            if (wotlk.MFBO != null)
                MFBO = new MFBO(wotlk.MFBO);
            else if (YetAnotherAdtConverter.Program.config.CreateMFBO)
            {
                UInt32 address = 0;
                MFBO = new MFBO();

                address += (UInt32)MHDR.Header.Byte_size;

                if(MH2O != null)
                {
                    address += (UInt32)MH2O.GetBytes().Length;
                }

                address += (UInt32)MCNKLength;
                MHDR.UpdateMFBO(address);
            }
        }

        public void WriteFile(DirectoryInfo directory)
        {
            Logger.log(ADTfileInfo.Name, Logger.Type.WRITE, directory.FullName);

            if(!directory.Exists)
            {
                directory.Create();
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(directory.FullName + "\\" + ADTfileInfo.Name, FileMode.Create)))
            {
                writer.Write(MVER.GetBytes());
                writer.Write(MHDR.GetBytes());

                if (MH2O != null)
                    writer.Write(MH2O.GetBytes());

                foreach (MCNK_ADT x in MCNKs)
                {
                    writer.Write(x.GetBytes());
                }

                if (MFBO != null)
                    writer.Write(MFBO.GetBytes());

                Logger.log(DateTime.Now.ToString(), Logger.Type.LEVEL1);
            }
        }
    }
}
