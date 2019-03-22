using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using YetAnotherAdtConverter.Files.WOTLK.Chunks;


namespace YetAnotherAdtConverter.Files.WOTLK
{
    [System.Diagnostics.DebuggerDisplay("{ADTfileInfo.Name} | {Length/1024}KB")]
    class ADT
    {
        public FileInfo ADTfileInfo;
        int Length;

        public MVER MVER;
        public MHDR MHDR;
        public MCIN MCIN;
        public MTEX MTEX;
        public MMDX MMDX;
        public MMID MMID;
        public MWMO MWMO;
        public MWID MWID;
        public MDDF MDDF;
        public MODF MODF;
        public MH2O MH2O;
        public List<MCNK> MCNKs = new List<MCNK>();
        public MFBO MFBO;

        public ADT(string filePath)
        {
            ADTfileInfo = new FileInfo(filePath);

            Logger.log(ADTfileInfo.Name, Logger.Type.READ, ADTfileInfo.DirectoryName);

            //Start reading the .ADT File
            using (BinaryReader reader = new BinaryReader(ADTfileInfo.Open(FileMode.Open)))
            {
                Length = (int)reader.BaseStream.Length;
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                int MCNK_counter = 0;
                int MCNK_size = 0;

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    byte[] ChunkMagic = reader.ReadBytes(4);
                    byte[] ChunkSize = reader.ReadBytes(4);
                    byte[] ChunkContent = reader.ReadBytes(BitConverter.ToInt32(ChunkSize,0));

                    string ChunkMagicString = MagicBytesToString(ChunkMagic);
                    //read the chunks
                    switch (ChunkMagicString)
                    {
                        case "MVER":
                            MVER = new MVER(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MHDR":
                            MHDR = new MHDR(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCIN":
                            MCIN = new MCIN(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MTEX":
                            MTEX = new MTEX(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MMDX":
                            MMDX = new MMDX(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MMID":
                            MMID = new MMID(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MWMO":
                            MWMO = new MWMO(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MWID":
                            MWID = new MWID(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MDDF":
                            MDDF = new MDDF(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MODF":
                            MODF = new MODF(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MH2O":
                            MH2O = new MH2O(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCNK":
                            MCNKs.Add(new MCNK(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent));
                            break;
                        case "MFBO":
                            MFBO = new MFBO(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                    }

                    if (ChunkMagicString == "MCNK") { MCNK_counter++; MCNK_size += BitConverter.ToInt32(ChunkSize,0); }
                    else if (ChunkMagicString == "\0\0\0\0") { /*Logger.log("0 Byte Chunk", Logger.Direction.WARNING);*/ }
                }

                if(MCNK_counter > 0)
                    Logger.log("MCNK[]", Logger.Type.LEVEL1, MCNK_size + " byte");
            }
        }

        static public char[] MagicBytesToChars(byte[] MagicBytes)
        {
            char[] MagicChars;
            MagicChars = new char[MagicBytes.Length];

            int count = 0;
            foreach (byte MagicByte in MagicBytes)
            {
                MagicChars[count] = Convert.ToChar(MagicByte);
                count++;
            }

            Array.Reverse(MagicChars);

            return MagicChars;
        }

        static public string MagicBytesToString(byte[] MagicBytes)
        {
            return new string(MagicBytesToChars(MagicBytes));
        }
    }
}
