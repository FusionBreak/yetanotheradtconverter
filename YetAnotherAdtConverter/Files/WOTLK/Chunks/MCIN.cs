using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MCIN : Chunk
{
    private readonly List<Entry> entries = new();

    public MCIN(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var entry = new Entry
            {
                adress = reader.ReadUInt32(),
                size = reader.ReadUInt32(),
                flags = reader.ReadUInt32(),
                asyncID = reader.ReadUInt32()
            };

            entries.Add(entry);
        }
    }
}