using YetAnotherAdtConverter.Files.Structs;
using YetAnotherAdtConverter.Files.WOTLK.Chunks;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MCNK_TEX0 : Chunk
{
    private readonly MCAL mcal;
    private readonly MCLY mcly;
    private readonly MCSH mcsh;

    public MCNK_TEX0(MCNK wotlk) : base(wotlk)
    {
        if (wotlk.Mcly != null)
            mcly = new MCLY(wotlk.Mcly);

        if (wotlk.Mcsh != null)
            mcsh = new MCSH(wotlk.Mcsh);

        if (wotlk.Mcal != null)
            mcal = new MCAL(wotlk.Mcal);

        Header.ChangeSize(RecalculateSize());
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        if (mcly != null)
            bytes.AddRange(mcly.GetBytes());

        if (mcsh != null)
            bytes.AddRange(mcsh.GetBytes());

        if (mcal != null)
            bytes.AddRange(mcal.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        var newSize = 0;

        if (mcly != null)
            newSize += mcly.GetBytes().Length;

        if (mcsh != null)
            newSize += mcsh.GetBytes().Length;

        if (mcal != null)
            newSize += mcal.GetBytes().Length;

        return newSize;
    }
}

internal class MCLY : Chunk
{
    private readonly List<MCLYentry> entries;

    public MCLY(WOTLK.Chunks.MCLY wotlk) : base(wotlk, true)
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

internal class MCSH : Chunk
{
    private readonly byte[] data;

    public MCSH(WOTLK.Chunks.MCSH wotlk) : base(wotlk, true)
    {
        data = wotlk.Data;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        bytes.AddRange(data);

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}

internal class MCAL : Chunk
{
    private readonly byte[] data;

    public MCAL(WOTLK.Chunks.MCAL wotlk) : base(wotlk, true)
    {
        data = wotlk.Data;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        bytes.AddRange(data);

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}