using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MODF : Chunk
{
    private readonly List<mapObjDef> mapObjDefs = new();

    public MODF(WOTLK.Chunks.MODF wotlk) : base(wotlk)
    {
        mapObjDefs = wotlk.MapObjDefs;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in mapObjDefs) bytes.AddRange(x.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}