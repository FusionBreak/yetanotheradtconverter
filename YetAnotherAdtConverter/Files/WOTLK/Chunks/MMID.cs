namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MMID : Chunk
{
    public MMID(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
            Offsets.Add(BitConverter.ToUInt32(reader.ReadBytes(4), 0));
    }

    public List<uint> Offsets { get; set; } = new();
}