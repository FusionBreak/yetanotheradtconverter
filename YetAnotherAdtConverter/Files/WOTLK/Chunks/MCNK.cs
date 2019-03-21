using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MCNK : Chunk
    {
        MCNKheader mcHeader;

        //Subchunks
        MCVT mcvt;
        MCCV mccv;
        MCNR mcnr;
        MCLY mcly;
        MCRF mcrf;
        MCAL mcal;
        MCSE mcse;

        MCSH mcsh;
        MCLQ mclq;      

        public MCNK(char[] magic, byte[] size, byte[] content) : base (magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                #region read MCNKhead
                mcHeader.Flags = reader.ReadUInt32();
                mcHeader.IndexX = reader.ReadUInt32();
                mcHeader.IndexY = reader.ReadUInt32();
                mcHeader.NLayers = reader.ReadUInt32();
                mcHeader.NDoodadRefs = reader.ReadUInt32();

                offset offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCVT = offset;

                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCNR = offset;
                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCLY = offset;
                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCRF = offset;
                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCAL = offset;

                mcHeader.SizeAlpha = reader.ReadUInt32();

                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCSH = offset;

                mcHeader.SizeShadow = reader.ReadUInt32();
                mcHeader.Areaid = reader.ReadUInt32();
                mcHeader.NMapObjRefs = reader.ReadUInt32();
                mcHeader.Holes = reader.ReadUInt32();

                mcHeader.Unk = new UInt32[4];
                mcHeader.Unk[0] = reader.ReadUInt32();
                mcHeader.Unk[1] = reader.ReadUInt32();
                mcHeader.Unk[2] = reader.ReadUInt32();
                mcHeader.Unk[3] = reader.ReadUInt32();

                mcHeader.PredTex = reader.ReadUInt32();
                mcHeader.NoEffectDoodad = reader.ReadUInt32();

                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCSE = offset;

                mcHeader.NSndEmitters = reader.ReadUInt32();

                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCLQ = offset;

                mcHeader.SizeLiquid = reader.ReadUInt32();

                mcHeader.Pos = new float[3];
                mcHeader.Pos[0] = reader.ReadSingle();
                mcHeader.Pos[1] = reader.ReadSingle();
                mcHeader.Pos[2] = reader.ReadSingle();

                offset = new offset();
                offset.address = reader.ReadUInt32();
                mcHeader.OfsMCCV = offset;

                mcHeader.Props = reader.ReadUInt32();
                mcHeader.EffectId = reader.ReadUInt32();
                #endregion

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    byte[] ChunkMagic = reader.ReadBytes(4);
                    byte[] ChunkSize = reader.ReadBytes(4);
                    byte[] ChunkContent = reader.ReadBytes(BitConverter.ToInt32(ChunkSize));

                    string ChunkMagicString = ADT.MagicBytesToString(ChunkMagic);

                    switch (ChunkMagicString)
                    {
                        case "MCVT":
                            mcvt = new MCVT(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCCV":
                            mccv = new MCCV(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCNR":
                            mcnr = new MCNR(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent, reader.ReadBytes(13));
                            break;
                        case "MCLY":
                            mcly = new MCLY(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCRF":
                            mcrf = new MCRF(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCAL":
                            mcal = new MCAL(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCSE":
                            mcse = new MCSE(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;

                        case "MCSH":
                            mcsh = new MCSH(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                        case "MCLQ":
                            mclq = new MCLQ(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                            break;
                    }

                    //Logger.log(ChunkMagicString, Logger.Direction.LEVEL2, ChunkContent.Length.ToString() + " byte");
                }
            }

            //Logger.log("---", Logger.Direction.LEVEL2);
        }

        internal MCVT Mcvt { get => mcvt; set => mcvt = value; }
        internal MCNR Mcnr { get => mcnr; set => mcnr = value; }
        internal MCLY Mcly { get => mcly; set => mcly = value; }
        internal MCRF Mcrf { get => mcrf; set => mcrf = value; }
        internal MCAL Mcal { get => mcal; set => mcal = value; }
        internal MCSE Mcse { get => mcse; set => mcse = value; }
        internal MCSH Mcsh { get => mcsh; set => mcsh = value; }
        internal MCLQ Mclq { get => mclq; set => mclq = value; }
        internal MCCV Mccv { get => mccv; set => mccv = value; }
        internal MCNKheader MCHeader { get => mcHeader; set => mcHeader = value; }
    }

    #region subchunks
    class MCVT : Chunk
    {
        float[] heightmap; // = new float[9*9 + 8*8];

        public MCVT(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            heightmap = new float[9 * 9 + 8 * 8]; // = [145]

            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                for (int x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++)
                {
                    heightmap[x] = reader.ReadSingle();
                }
            }
        }

        public float[] Heightmap { get => heightmap; set => heightmap = value; }
    }

    class MCNR : Chunk
    {
        MCNRentry[] entries; // = new MCNRentry[9*9 + 8*8];

        byte[] padding;  /* = new byte[13]; | this data is not included in the MCNR chunk but additional data which purpose is unknown. 0.5.3.3368 lists this as padding
         always 0 112 245  18 0  8 0 0  0 84  245 18 0. Nobody yet found a different pattern. The data is not derived from the normals.
         It also does not seem that the client reads this data. --Schlumpf (talk) 23:01, 26 July 2015 (UTC)
         While stated that this data is not "included in the MCNR chunk", the chunk-size defined for the MCNR chunk does cover this data. --Kruithne Feb 2016
         ... from Cataclysm only (on LK files and before, MCNR defined size is 435 and not 448) Mjollna (talk) */

        public MCNR(char[] magic, byte[] size, byte[] content, byte[] pad) : base(magic, size, true)
        {
            entries = new MCNRentry[9 * 9 + 8 * 8]; //= [145]
            padding = pad;

            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                for (int x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++)
                {
                    entries[x].X = reader.ReadByte();
                    entries[x].Z = reader.ReadByte();
                    entries[x].Y = reader.ReadByte();
                }
            }
        }

        public byte[] Padding { get => padding; set => padding = value; }
        internal MCNRentry[] Entries { get => entries; set => entries = value; }
    }

    class MCLY : Chunk
    {
        List<MCLYentry> entries;

        public MCLY(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            entries = new List<MCLYentry>();
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                for(int x = 0; (reader.BaseStream.Position < reader.BaseStream.Length) && x < 4; x++)
                {
                    MCLYentry entry = new MCLYentry();
                    entry.Textid = reader.ReadUInt32();
                    entry.Flags = reader.ReadUInt32();
                    entry.Ofsalphamap = reader.ReadUInt32();
                    entry.Detailtextureid = reader.ReadUInt32();
                    entries.Add(entry);
                }
            }
        }

        internal List<MCLYentry> Entries { get => entries; set => entries = value; }
    }

    

    [System.Diagnostics.DebuggerDisplay("Doodad Count: {doodads.Length}")]
    class MCRF : Chunk
    {
        UInt32[] doodads;
        public MCRF(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            doodads = new UInt32[content.Length/4]; //4 = Length of a UInt32

            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                for (int x = 0;reader.BaseStream.Position < reader.BaseStream.Length; x++)
                {
                    doodads[x] = reader.ReadUInt32();
                }               
            }
        }

        public uint[] Doodads { get => doodads; set => doodads = value; }
    }

    class MCAL : Chunk
    {
        byte[] data;

        public MCAL(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            data = content;
        }

        public byte[] Data { get => data; set => data = value; }
    }

    [System.Diagnostics.DebuggerDisplay("Count: {count}")]
    class MCSE : Chunk
    {
        MCSEentry[] entries;
        int count = 0;

        public MCSE(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            
            if(content.Length / 28 > 0)
            {
                entries = new MCSEentry[content.Length / 28]; //28 = Byte-length of a MCSEentry

                using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
                {
                    for (int x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++)
                    {
                        entries[x].Dbcidd = reader.ReadUInt32();

                        Vector vector = new Vector();
                        vector.X = reader.ReadSingle();
                        vector.Y = reader.ReadSingle();
                        vector.Z = reader.ReadSingle();
                        entries[x].Position = vector;

                        vector = new Vector();
                        vector.X = reader.ReadSingle();
                        vector.Y = reader.ReadSingle();
                        vector.Z = reader.ReadSingle();
                        entries[x].Radius = vector;

                    }
                }

                count = entries.Length;
            }
        }

        public int Count { get => count; set => count = value; }
        internal MCSEentry[] Entries { get => entries; set => entries = value; }
    }

    class MCSH : Chunk
    {
        byte[] data;

        public MCSH(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            data = content;
        }

        public byte[] Data { get => data; set => data = value; }
    }

    class MCLQ : Chunk
    {
        byte[] data;

        public MCLQ(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            data = content;
        }
    }

    class MCCV : Chunk
    {
        MCCVentry[] entries;

        public MCCV(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
        {
            entries = new MCCVentry[9 * 9 + 8 * 8]; //= [145]

            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                for (int x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++)
                {
                    entries[x].Blue = reader.ReadByte();
                    entries[x].Green = reader.ReadByte();
                    entries[x].Red = reader.ReadByte();
                    entries[x].Alpha = reader.ReadByte();
                }
            }
        }

        internal MCCVentry[] Entries { get => entries; set => entries = value; }
    }
    #endregion
}
