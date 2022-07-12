namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     Only found in an MH2O chunk. Always an array of 256 elements.
///     <para>This structure has a fixed size of 12 bytes.</para>
/// </summary>
internal struct MH2O_Header
{
    public uint Offset_instances { get; set; }
    public uint Layer_count { get; set; }
    public uint Offset_attributes { get; set; }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(Offset_attributes));
        bytes.AddRange(BitConverter.GetBytes(Layer_count));
        bytes.AddRange(BitConverter.GetBytes(Offset_instances));

        return bytes.ToArray();
    }
}