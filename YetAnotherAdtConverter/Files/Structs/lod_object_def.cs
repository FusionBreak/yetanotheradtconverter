namespace YetAnotherAdtConverter.Files.Structs;

internal struct lod_object_def
{
    private Vector position;
    private Vector rotation;

    public uint NameID { get; set; }
    public uint UniqueID { get; set; }
    public ushort Flags { get; set; }
    public ushort DoodadSet { get; set; }
    public ushort NameSet { get; set; }
    public ushort Unk { get; set; }

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
        bytes.AddRange(BitConverter.GetBytes(Flags));
        bytes.AddRange(BitConverter.GetBytes(DoodadSet));
        bytes.AddRange(BitConverter.GetBytes(NameSet));
        bytes.AddRange(BitConverter.GetBytes(Unk));

        return bytes.ToArray();
    }
}