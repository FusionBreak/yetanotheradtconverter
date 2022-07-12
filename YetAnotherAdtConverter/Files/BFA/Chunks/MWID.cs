namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MWID : Chunk
{
    private readonly List<uint> offsets = new();

    public MWID(WOTLK.Chunks.MWID wotlk) : base(wotlk)
    {
        offsets = wotlk.Offsets;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (int x in offsets) bytes.AddRange(BitConverter.GetBytes(x));

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}