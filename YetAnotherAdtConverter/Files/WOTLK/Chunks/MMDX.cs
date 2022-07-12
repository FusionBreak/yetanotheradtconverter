using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

[DebuggerDisplay("{m2s.Count} Files")]
internal class MMDX : Chunk
{
    public MMDX(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var m2 = "";

            for (var x = reader.ReadByte(); x != 0x00; x = reader.ReadByte()) m2 += Convert.ToChar(x);

            M2s.Add(m2);
        }
    }

    public List<string> M2s { get; set; } = new();
}