using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

[DebuggerDisplay("{wmos.Count} Files")]
internal class MWMO : Chunk
{
    public MWMO(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var wmo = "";

            for (var x = reader.ReadByte(); x != 0x00; x = reader.ReadByte()) wmo += Convert.ToChar(x);

            Wmos.Add(wmo);
        }
    }

    public List<string> Wmos { get; set; } = new();
}