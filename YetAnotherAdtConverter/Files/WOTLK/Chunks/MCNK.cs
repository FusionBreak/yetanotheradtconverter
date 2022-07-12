using System.Diagnostics;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MCNK : Chunk
{
    private MCNKheader mcHeader;

    public MCNK(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));

        #region read MCNKhead

        mcHeader.Flags = reader.ReadUInt32();
        mcHeader.IndexX = reader.ReadUInt32();
        mcHeader.IndexY = reader.ReadUInt32();
        mcHeader.NLayers = reader.ReadUInt32();
        mcHeader.NDoodadRefs = reader.ReadUInt32();

        var offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCVT = offset;

        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCNR = offset;
        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCLY = offset;
        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCRF = offset;
        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCAL = offset;

        mcHeader.SizeAlpha = reader.ReadUInt32();

        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCSH = offset;

        mcHeader.SizeShadow = reader.ReadUInt32();
        mcHeader.Areaid = reader.ReadUInt32();
        mcHeader.NMapObjRefs = reader.ReadUInt32();
        mcHeader.Holes = reader.ReadUInt32();

        mcHeader.GroundEffectsMap = new byte[16];
        for (var x = 0; x < 16; x++) mcHeader.GroundEffectsMap[x] = reader.ReadByte();

        mcHeader.PredTex = reader.ReadUInt32();
        mcHeader.NoEffectDoodad = reader.ReadUInt32();

        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCSE = offset;

        mcHeader.NSndEmitters = reader.ReadUInt32();

        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCLQ = offset;

        mcHeader.SizeLiquid = reader.ReadUInt32();

        mcHeader.Pos = new float[3];
        mcHeader.Pos[0] = reader.ReadSingle();
        mcHeader.Pos[1] = reader.ReadSingle();
        mcHeader.Pos[2] = reader.ReadSingle();

        offset = new offset
        {
            address = reader.ReadUInt32()
        };
        mcHeader.OfsMCCV = offset;

        mcHeader.Props = reader.ReadUInt32();
        mcHeader.EffectId = reader.ReadUInt32();

        #endregion

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var ChunkMagic = reader.ReadBytes(4);
            var ChunkSize = reader.ReadBytes(4);
            var ChunkContent = reader.ReadBytes(BitConverter.ToInt32(ChunkSize, 0));

            var ChunkMagicString = ADT.MagicBytesToString(ChunkMagic);

            switch (ChunkMagicString)
            {
                case "MCVT":
                    Mcvt = new MCVT(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCCV":
                    Mccv = new MCCV(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCNR":
                    Mcnr = new MCNR(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent, reader.ReadBytes(13));
                    break;
                case "MCLY":
                    Mcly = new MCLY(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCRF":
                    Mcrf = new MCRF(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCAL":
                    Mcal = new MCAL(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCSE":
                    Mcse = new MCSE(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;

                case "MCSH":
                    Mcsh = new MCSH(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCLQ":
                    Mclq = new MCLQ(ADT.MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
            }

            //Logger.log(ChunkMagicString, Logger.Direction.LEVEL2, ChunkContent.Length.ToString() + " byte");
        }

        //Logger.log("---", Logger.Direction.LEVEL2);
    }

    internal MCVT Mcvt { get; set; }
    internal MCNR Mcnr { get; set; }
    internal MCLY Mcly { get; set; }
    internal MCRF Mcrf { get; set; }
    internal MCAL Mcal { get; set; }
    internal MCSE Mcse { get; set; }
    internal MCSH Mcsh { get; set; }
    internal MCLQ Mclq { get; set; }
    internal MCCV Mccv { get; set; }

    internal MCNKheader MCHeader
    {
        get => mcHeader;
        set => mcHeader = value;
    }
}

#region subchunks

internal class MCVT : Chunk
{
    public MCVT(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        Heightmap = new float[9 * 9 + 8 * 8]; // = [145]

        using var reader = new BinaryReader(new MemoryStream(content));
        for (var x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++) Heightmap[x] = reader.ReadSingle();
    }

    public float[] Heightmap { get; set; }
}

internal class MCNR : Chunk
{
    public MCNR(char[] magic, byte[] size, byte[] content, byte[] pad) : base(magic, size, true)
    {
        Entries = new MCNRentry[9 * 9 + 8 * 8]; //= [145]
        Padding = pad;

        using var reader = new BinaryReader(new MemoryStream(content));
        for (var x = 0; reader.BaseStream.Position < reader.BaseStream.Length && x < 145; x++)
        {
            Entries[x].X = reader.ReadByte();
            Entries[x].Z = reader.ReadByte();
            Entries[x].Y = reader.ReadByte();
        }
    }

    public byte[] Padding { get; set; }
    internal MCNRentry[] Entries { get; set; }
}

internal class MCLY : Chunk
{
    public MCLY(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        Entries = new List<MCLYentry>();
        using var reader = new BinaryReader(new MemoryStream(content));
        for (var x = 0; reader.BaseStream.Position < reader.BaseStream.Length && x < 4; x++)
        {
            var entry = new MCLYentry
            {
                Textid = reader.ReadUInt32(),
                Flags = reader.ReadUInt32(),
                Ofsalphamap = reader.ReadUInt32(),
                Detailtextureid = reader.ReadUInt32()
            };

            Entries.Add(entry);
        }
    }

    internal List<MCLYentry> Entries { get; set; }
}

[DebuggerDisplay("Doodad Count: {doodads.Length}")]
internal class MCRF : Chunk
{
    public MCRF(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        Doodads = new uint[content.Length / 4]; //4 = Length of a UInt32

        using var reader = new BinaryReader(new MemoryStream(content));
        for (var x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++) Doodads[x] = reader.ReadUInt32();
    }

    public uint[] Doodads { get; set; }
}

internal class MCAL : Chunk
{
    public MCAL(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        Data = content;
    }

    public byte[] Data { get; set; }
}

[DebuggerDisplay("Count: {count}")]
internal class MCSE : Chunk
{
    public MCSE(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        if (content.Length / 28 > 0)
        {
            Entries = new MCSEentry[content.Length / 28]; //28 = Byte-length of a MCSEentry

            using (var reader = new BinaryReader(new MemoryStream(content)))
            {
                for (var x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++)
                {
                    Entries[x].Dbcidd = reader.ReadUInt32();

                    var vector = new Vector
                    {
                        X = reader.ReadSingle(),
                        Y = reader.ReadSingle(),
                        Z = reader.ReadSingle()
                    };
                    Entries[x].Position = vector;

                    vector = new Vector
                    {
                        X = reader.ReadSingle(),
                        Y = reader.ReadSingle(),
                        Z = reader.ReadSingle()
                    };
                    Entries[x].Radius = vector;
                }
            }

            Count = Entries.Length;
        }
    }

    public int Count { get; set; }
    internal MCSEentry[] Entries { get; set; }
}

internal class MCSH : Chunk
{
    public MCSH(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        Data = content;
    }

    public byte[] Data { get; set; }
}

internal class MCLQ : Chunk
{
    private readonly byte[] data;

    public MCLQ(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        data = content;
    }
}

internal class MCCV : Chunk
{
    public MCCV(char[] magic, byte[] size, byte[] content) : base(magic, size, true)
    {
        Entries = new MCCVentry[9 * 9 + 8 * 8]; //= [145]

        using var reader = new BinaryReader(new MemoryStream(content));
        for (var x = 0; reader.BaseStream.Position < reader.BaseStream.Length; x++)
        {
            Entries[x].Blue = reader.ReadByte();
            Entries[x].Green = reader.ReadByte();
            Entries[x].Red = reader.ReadByte();
            Entries[x].Alpha = reader.ReadByte();
        }
    }

    internal MCCVentry[] Entries { get; set; }
}

#endregion