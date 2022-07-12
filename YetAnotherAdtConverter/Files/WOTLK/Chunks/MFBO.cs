using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MFBO : Chunk
{
    private Plane maximum;
    private Plane minimum;

    public MFBO(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        maximum.Height = new short[3][];
        for (var x = 0; x < 3; x++)
        {
            maximum.Height[x] = new short[3];
            for (var y = 0; y < 3; y++) maximum.Height[x][y] = reader.ReadInt16();
        }

        minimum.Height = new short[3][];
        for (var x = 0; x < 3; x++)
        {
            minimum.Height[x] = new short[3];
            for (var y = 0; y < 3; y++) minimum.Height[x][y] = reader.ReadInt16();
        }
    }

    internal Plane Maximum
    {
        get => maximum;
        set => maximum = value;
    }

    internal Plane Minimum
    {
        get => minimum;
        set => minimum = value;
    }
}