using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     <para>This structure has a fixed size of 12 bytes.</para>
/// </summary>
[DebuggerDisplay("{x}, {y}, {z}")]
internal struct Vector
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(X));
        bytes.AddRange(BitConverter.GetBytes(Y));
        bytes.AddRange(BitConverter.GetBytes(Z));

        return bytes.ToArray();
    }
}