using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MDDF : Chunk
{
    private readonly List<DoodadDef> doodadDefs = new();

    public MDDF(WOTLK.Chunks.MDDF wotlk) : base(wotlk)
    {
        doodadDefs = wotlk.DoodadDefs;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in doodadDefs) bytes.AddRange(x.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}