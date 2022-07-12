using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MWMO : Chunk
{
    private readonly List<string> wmos = new();

    public MWMO(WOTLK.Chunks.MWMO wotlk) : base(wotlk)
    {
        wmos = wotlk.Wmos;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in wmos)
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