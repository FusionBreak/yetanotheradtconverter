namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     The function of this structure did not come to me, so it is simply called "data". ¯\_(ツ)_/¯
///     <para>This structure has a fixed size of 16 bytes.</para>
/// </summary>
internal struct data2
{
    public byte[] A { get; set; }
    public byte[] B { get; set; }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(A);
        bytes.AddRange(B);

        return bytes.ToArray();
    }
}