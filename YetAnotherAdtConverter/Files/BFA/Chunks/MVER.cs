namespace YetAnotherAdtConverter.Files.BFA.Chunks;

//[System.Diagnostics.DebuggerDisplay("Version: {version}")]
internal class MVER : Chunk
{
    private readonly byte[] version;

    public MVER(WOTLK.Chunks.MVER wotlk) : base(wotlk)
    {
        version = wotlk.version;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());
        bytes.AddRange(version);

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
        //Not needed, because fixed size : 4 byte
    }
}