namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MWID : Chunk
{
    public MWID(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length) Offsets.Add(reader.ReadUInt32());
    }

    public List<uint> Offsets { get; set; } = new();
}