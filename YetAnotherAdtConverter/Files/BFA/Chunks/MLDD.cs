using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MLDD : Chunk
{
    private readonly List<DoodadDef> doodadDefs = new();

    public MLDD(WOTLK.Chunks.MDDF wotlk) : base(wotlk, "MLDD")
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