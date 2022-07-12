using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

[DebuggerDisplay("{textures.Count} Files")]
internal class MTEX : Chunk
{
    public MTEX(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var texture = "";

            for (var x = reader.ReadByte(); x != 0x00; x = reader.ReadByte()) texture += Convert.ToChar(x);

            Textures.Add(texture);
        }
    }

    public List<string> Textures { get; set; } = new();
}