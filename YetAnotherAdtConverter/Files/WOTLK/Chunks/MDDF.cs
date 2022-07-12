using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MDDF : Chunk
{
    public MDDF(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var entry = new DoodadDef();
            var pos = new Vector();
            var rot = new Vector();

            entry.NameID = reader.ReadUInt32();
            entry.UniqueID = reader.ReadUInt32();

            pos.X = reader.ReadSingle();
            pos.Y = reader.ReadSingle();
            pos.Z = reader.ReadSingle();
            entry.Position = pos;

            rot.X = reader.ReadSingle();
            rot.Y = reader.ReadSingle();
            rot.Z = reader.ReadSingle();
            entry.Rotation = rot;

            entry.Scale = reader.ReadUInt16();
            entry.Flags = reader.ReadUInt16();

            DoodadDefs.Add(entry);
        }
    }

    internal List<DoodadDef> DoodadDefs { get; set; } = new();
}