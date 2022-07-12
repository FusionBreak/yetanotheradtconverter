using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MFBO : Chunk
{
    private Plane maximum;
    private Plane minimum;

    public MFBO(WOTLK.Chunks.MFBO wotlk) : base(wotlk)
    {
        maximum = wotlk.Maximum;
        minimum = wotlk.Minimum;
    }

    public MFBO() : base("MFBO", 36)
    {
        maximum.Height = new short[3][];
        minimum.Height = new short[3][];
        for (var x = 0; x < 3; x++)
        {
            maximum.Height[x] = new short[3];
            minimum.Height[x] = new short[3];
            for (var y = 0; y < 3; y++)
            {
                maximum.Height[x][y] = 900;
                minimum.Height[x][y] = -0x96;
            }
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

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        bytes.AddRange(maximum.GetBytes());
        bytes.AddRange(minimum.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}