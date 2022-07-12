using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.BFA;

[DebuggerDisplay("{Header.Byte_size} byte")]
internal abstract class Chunk
{
    public ChunkHeader Header;

    public Chunk(WOTLK.Chunk wotlk, bool isSub = false)
    {
        Header = new ChunkHeader(wotlk.Header.Magic, wotlk.Header.Size);
    }

    public Chunk(WOTLK.Chunk wotlk, string magic, bool isSub = false)
    {
        Header = new ChunkHeader(magic.ToCharArray(), wotlk.Header.Size);
    }

    public Chunk(string magic, int size, bool isSub = false)
    {
        Header = new ChunkHeader(magic.ToCharArray(), BitConverter.GetBytes(size));
    }

    public float ConvertClientCoordsToServerCoords(float client)
    {
        var server = 17066 - client;
        return server;
    }

    public abstract int RecalculateSize();

    public abstract byte[] GetBytes();
}

internal class ChunkHeader
{
    private readonly char[] Magic;
    public int Byte_size;
    private byte[] Size;

    public ChunkHeader(char[] magic, byte[] size)
    {
        Magic = magic;
        Size = size;
        Byte_size = BitConverter.ToInt32(size, 0);
    }

    public void AddSize(int x)
    {
        var old = BitConverter.ToInt32(Size, 0);
        Size = BitConverter.GetBytes(old + x);
        Byte_size = BitConverter.ToInt32(Size, 0);
    }

    public void ChangeSize(int x)
    {
        Size = BitConverter.GetBytes(x);
        Byte_size = BitConverter.ToInt32(Size, 0);
    }

    public byte[] GetBytes()
    {
        var bytes = new byte[8];

        var magicBytes = Magic;

        bytes[0] = Convert.ToByte(magicBytes[3]);
        bytes[1] = Convert.ToByte(magicBytes[2]);
        bytes[2] = Convert.ToByte(magicBytes[1]);
        bytes[3] = Convert.ToByte(magicBytes[0]);

        bytes[4] = Size[0];
        bytes[5] = Size[1];
        bytes[6] = Size[2];
        bytes[7] = Size[3];

        return bytes;
    }

    public string GetHeaderString()
    {
        return new(Magic);
    }
}