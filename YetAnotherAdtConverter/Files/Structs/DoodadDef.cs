using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

[DebuggerDisplay("uniqueID: {uniqueID}")]
internal struct DoodadDef
{
    private Vector position;
    private Vector rotation;

    public uint NameID { get; set; }
    public uint UniqueID { get; set; }
    public ushort Scale { get; set; }
    public ushort Flags { get; set; }

    internal Vector Position
    {
        get => position;
        set => position = value;
    }

    internal Vector Rotation
    {
        get => rotation;
        set => rotation = value;
    }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(NameID));
        bytes.AddRange(BitConverter.GetBytes(UniqueID));
        bytes.AddRange(position.GetBytes());
        bytes.AddRange(rotation.GetBytes());
        bytes.AddRange(BitConverter.GetBytes(Scale));
        bytes.AddRange(BitConverter.GetBytes(Flags));

        return bytes.ToArray();
    }
}