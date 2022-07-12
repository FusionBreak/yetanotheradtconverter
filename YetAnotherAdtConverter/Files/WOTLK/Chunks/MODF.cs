using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MODF : Chunk
{
    public MODF(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var entry = new mapObjDef();
            var pos = new Vector();
            var rot = new Vector();
            var ext = new CAaBox();
            var min = new Vector();
            var max = new Vector();

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

            min.X = reader.ReadSingle();
            min.Y = reader.ReadSingle();
            min.Z = reader.ReadSingle();
            max.X = reader.ReadSingle();
            max.Y = reader.ReadSingle();
            max.Z = reader.ReadSingle();
            ext.Min = min;
            ext.Max = max;
            entry.Extents = ext;

            entry.Flags = reader.ReadUInt16();
            entry.DoodadSet = reader.ReadUInt16();
            entry.NameSet = reader.ReadUInt16();
            entry.Scale = reader.ReadUInt16();

            MapObjDefs.Add(entry);
        }
    }

    internal List<mapObjDef> MapObjDefs { get; set; } = new();
}