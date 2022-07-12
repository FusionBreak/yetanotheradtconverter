namespace YetAnotherAdtConverter.Files.Structs;

internal struct Plane
{
    public byte[] GetBytes()
    {
        var bytes = new List<byte>();
        for (var x = 0; x < 3; x++)
        for (var y = 0; y < 3; y++)
            bytes.AddRange(BitConverter.GetBytes(Height[x][y])); //

        return bytes.ToArray();
    }

    public short[][] Height { get; set; }
}