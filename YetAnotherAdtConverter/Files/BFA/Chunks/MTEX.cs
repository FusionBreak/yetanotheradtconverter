using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MTEX : Chunk
{
    private readonly List<string> textures = new();

    public MTEX(WOTLK.Chunks.MTEX wotlk, bool isSub = false) : base(wotlk, isSub)
    {
        textures = wotlk.Textures;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in textures)
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