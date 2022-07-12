namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     Only found in an MCSE chunk. Always an array of 145 (9*9 + 8*8) elements.
///     <para>This structure has a fixed size of 3 bytes.</para>
/// </summary>
internal struct MCSEentry
{
    private Vector position;
    private Vector radius;

    public uint Dbcidd { get; set; }

    internal Vector Position
    {
        get => position;
        set => position = value;
    }

    internal Vector Radius
    {
        get => radius;
        set => radius = value;
    }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(Dbcidd));
        bytes.AddRange(position.GetBytes());
        bytes.AddRange(radius.GetBytes());

        return bytes.ToArray();
    }
}