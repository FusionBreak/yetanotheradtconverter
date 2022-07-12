using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

[DebuggerDisplay("TextureID: {textid}")]
internal struct MCLYentry
{
    public uint Textid { get; set; }
    public uint Flags { get; set; }
    public uint Ofsalphamap { get; set; }
    public uint Detailtextureid { get; set; }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(Textid));
        bytes.AddRange(BitConverter.GetBytes(Flags));
        bytes.AddRange(BitConverter.GetBytes(Ofsalphamap));
        bytes.AddRange(BitConverter.GetBytes(Detailtextureid));

        return bytes.ToArray();
    }
}