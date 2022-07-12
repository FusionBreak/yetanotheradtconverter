using YetAnotherAdtConverter.Files.WOTLK.Chunks;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MCNK_OBJ0 : Chunk
{
    private readonly MCRD mcrd;
    private readonly MCRW mcrw;
    private readonly int NDoodadRefs;
    private readonly int NMapObjRefs;

    public MCNK_OBJ0(MCNK wotlk) : base(wotlk)
    {
        NDoodadRefs = (int)wotlk.MCHeader.NDoodadRefs;
        NMapObjRefs = (int)wotlk.MCHeader.NMapObjRefs;

        if (NDoodadRefs > 0)
        {
            var doodad = new uint[NDoodadRefs];

            for (var x = 0; x < NDoodadRefs; x++) doodad[x] = wotlk.Mcrf.Doodads[x];

            mcrd = new MCRD("MCRD", doodad.Length * 4, doodad);
        }

        if (NMapObjRefs > 0)
        {
            var doodad = new uint[NMapObjRefs];

            for (var x = 0; x < NMapObjRefs; x++) doodad[x] = wotlk.Mcrf.Doodads[x + NDoodadRefs];

            mcrw = new MCRW("MCRW", doodad.Length * 4, doodad);
        }

        Header.ChangeSize(RecalculateSize());
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        if (mcrd != null)
            bytes.AddRange(mcrd.GetBytes());

        if (mcrw != null)
            bytes.AddRange(mcrw.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        var newSize = 0;

        if (mcrd != null)
            newSize += mcrd.GetBytes().Length;

        if (mcrw != null)
            newSize += mcrw.GetBytes().Length;

        return newSize;
    }
}

internal class MCRD : Chunk
{
    private readonly uint[] doodads;

    public MCRD(string magic, int size, uint[] content) : base(magic, size, true)
    {
        doodads = content;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in doodads) bytes.AddRange(BitConverter.GetBytes(x));

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}

internal class MCRW : Chunk
{
    private readonly uint[] doodads;

    public MCRW(string magic, int size, uint[] content) : base(magic, size, true)
    {
        doodads = content;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in doodads) bytes.AddRange(BitConverter.GetBytes(x));

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}