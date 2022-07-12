using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     Only found in an MCNR chunk. Always an array of 145 (9*9 + 8*8) elements.
///     <para>This structure has a fixed size of 3 bytes.</para>
/// </summary>
[DebuggerDisplay("X:{x} | Y:{y} | Z:{z}")]
internal struct MCNRentry
{
    public byte X { get; set; }
    public byte Z { get; set; }
    public byte Y { get; set; }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>
        {
            X,
            Z,
            Y
        };

        return bytes.ToArray();
    }
}