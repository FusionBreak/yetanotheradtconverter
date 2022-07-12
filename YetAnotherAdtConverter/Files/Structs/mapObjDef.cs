using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     <para>Placement information for WMOs. Additional to this, the WMOs to render are referenced in each MCRF chunk.</para>
/// </summary>
[DebuggerDisplay("uniqueID: {uniqueID}")]
internal struct mapObjDef
{
    private Vector position;
    private Vector rotation;
    private CAaBox extents;

    public uint NameID { get; set; }
    public uint UniqueID { get; set; }
    public ushort Scale { get; set; }
    public ushort Flags { get; set; }
    public ushort DoodadSet { get; set; }
    public ushort NameSet { get; set; }

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

    internal CAaBox Extents
    {
        get => extents;
        set => extents = value;
    }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(NameID));
        bytes.AddRange(BitConverter.GetBytes(UniqueID));
        bytes.AddRange(position.GetBytes());
        bytes.AddRange(rotation.GetBytes());
        bytes.AddRange(extents.GetBytes());
        bytes.AddRange(BitConverter.GetBytes(Flags));
        bytes.AddRange(BitConverter.GetBytes(DoodadSet));
        bytes.AddRange(BitConverter.GetBytes(NameSet));
        bytes.AddRange(BitConverter.GetBytes(Scale));

        return bytes.ToArray();
    }
}