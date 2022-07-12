using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.WOTLK;

[DebuggerDisplay("{Header.Byte_size} byte")]
internal class Chunk
{
    public ChunkHeader Header;

    public Chunk(char[] magic, byte[] size, bool isSub = false)
    {
        Header = new ChunkHeader(magic, size);
    }
}

internal class ChunkHeader
{
    public int Byte_size;
    public char[] Magic;
    public byte[] Size;

    public ChunkHeader(char[] magic, byte[] size)
    {
        Magic = magic;
        Size = size;
        Byte_size = BitConverter.ToInt32(size, 0);
        //Array.Reverse(Magic);
    }

    public string GetHeaderString()
    {
        return new(Magic);
    }
}