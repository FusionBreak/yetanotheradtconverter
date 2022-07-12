using System.Diagnostics;
using YetAnotherAdtConverter.Files.Structs;
using YetAnotherAdtConverter.Files.WOTLK.Chunks;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MCNK_ADT : Chunk
{
    private readonly MCCV mccv;
    private readonly MCNR mcnr;
    private readonly MCSE mcse;

    //Subchunks
    private readonly MCVT mcvt;
    private MCNKheader mcHeader;

    public MCNK_ADT(MCNK wotlk) : base(wotlk)
    {
        mcHeader = wotlk.MCHeader;

        mcvt = new MCVT(wotlk.Mcvt);

        if (wotlk.Mccv != null)
            mccv = new MCCV(wotlk.Mccv);

        mcnr = new MCNR(wotlk.Mcnr);

        if (wotlk.Mcse != null)
            mcse = new MCSE(wotlk.Mcse);

        Header.ChangeSize(RecalculateSize());
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        bytes.AddRange(mcHeader.GetBytes());

        bytes.AddRange(mcvt.GetBytes());

        if (mccv != null)
            bytes.AddRange(mccv.GetBytes());

        bytes.AddRange(mcnr.GetBytes());

        if (mcse != null)
            bytes.AddRange(mcse.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        var newSize = 0;

        newSize += mcvt.GetBytes().Length;

        if (mccv != null)
            newSize += mccv.GetBytes().Length;

        newSize += mcnr.GetBytes().Length;

        if (mcse != null)
            newSize += mcse.GetBytes().Length;

        newSize += 128; //128 bytes for the MCNKheader

        return newSize;
    }
}

#region subchunks

internal class MCVT : Chunk
{
    private readonly float[] heightmap; // = new float[9*9 + 8*8];

    public MCVT(WOTLK.Chunks.MCVT wotlk) : base(wotlk, true)
    {
        heightmap = wotlk.Heightmap;
        // = [145]
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        foreach (var x in heightmap) bytes.AddRange(BitConverter.GetBytes(x));

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}

internal class MCCV : Chunk
{
    private readonly MCCVentry[] entries;

    public MCCV(WOTLK.Chunks.MCCV wotlk) : base(wotlk, true)
    {
        entries = wotlk.Entries;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        foreach (var x in entries) bytes.AddRange(x.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}

internal class MCNR : Chunk
{
    private readonly MCNRentry[] entries; // = new MCNRentry[9*9 + 8*8];

    private readonly byte[]
        padding; /* = new byte[13]; | this data is not included in the MCNR chunk but additional data which purpose is unknown. 0.5.3.3368 lists this as padding
         always 0 112 245  18 0  8 0 0  0 84  245 18 0. Nobody yet found a different pattern. The data is not derived from the normals.
         It also does not seem that the client reads this data. --Schlumpf (talk) 23:01, 26 July 2015 (UTC)
         While stated that this data is not "included in the MCNR chunk", the chunk-size defined for the MCNR chunk does cover this data. --Kruithne Feb 2016
         ... from Cataclysm only (on LK files and before, MCNR defined size is 435 and not 448) Mjollna (talk) */

    public MCNR(WOTLK.Chunks.MCNR wotlk) : base(wotlk, true)
    {
        entries = wotlk.Entries;
        padding = wotlk.Padding;

        Header.AddSize(13);
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        foreach (var x in entries) bytes.AddRange(x.GetBytes());

        bytes.AddRange(padding);

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}

[DebuggerDisplay("Count: {count}")]
internal class MCSE : Chunk
{
    private readonly int count;
    private readonly MCSEentry[] entries;

    public MCSE(WOTLK.Chunks.MCSE wotlk) : base(wotlk, true)
    {
        entries = wotlk.Entries;
        count = wotlk.Count;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        if (count > 0)
            foreach (var x in entries)
                bytes.AddRange(x.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}

#endregion