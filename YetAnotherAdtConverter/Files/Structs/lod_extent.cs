namespace YetAnotherAdtConverter.Files.Structs;

internal struct lod_extent
{
    private CAaBox bounding;

    public float Radius { get; set; }

    internal CAaBox Bounding
    {
        get => bounding;
        set => bounding = value;
    }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(bounding.GetBytes());
        bytes.AddRange(BitConverter.GetBytes(Radius));

        return bytes.ToArray();
    }
}