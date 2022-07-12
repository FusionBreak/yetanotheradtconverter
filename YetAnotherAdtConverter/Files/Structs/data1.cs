namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     The function of this structure did not come to me, so it is simply called "data". ¯\_(ツ)_/¯
///     <para>This structure has a fixed size of 24 bytes.</para>
/// </summary>
internal struct data1
{
    public ushort Flags { get; set; }
    public ushort Type { get; set; }
    public float Height1 { get; set; }
    public float Height2 { get; set; }
    public byte Xoff { get; set; }
    public byte Yoff { get; set; }
    public byte W { get; set; }
    public byte H { get; set; }
    public uint Of2a { get; set; }
    public uint Of2b { get; set; }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(Flags));
        bytes.AddRange(BitConverter.GetBytes(Type));
        bytes.AddRange(BitConverter.GetBytes(Height1));
        bytes.AddRange(BitConverter.GetBytes(Height2));

        bytes.Add(Xoff);
        bytes.Add(Yoff);
        bytes.Add(W);
        bytes.Add(H);

        bytes.AddRange(BitConverter.GetBytes(Of2a));
        bytes.AddRange(BitConverter.GetBytes(Of2b));

        return bytes.ToArray();
    }
}