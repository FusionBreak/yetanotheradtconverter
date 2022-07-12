using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MMDX : Chunk
{
    private readonly List<string> m2s = new();

    public MMDX(WOTLK.Chunks.MMDX wotlk) : base(wotlk)
    {
        m2s = wotlk.M2s;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in m2s)
        {
            bytes.AddRange(Encoding.ASCII.GetBytes(x));
            bytes.Add(0x0);
        }

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}