using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

/// <summary>
///     Only found in an MCCV chunk. Always an array of 145 (9*9 + 8*8) elements.
///     <para>This structure has a fixed size of 4 bytes.</para>
/// </summary>
[DebuggerDisplay("R:{red} | G:{green} | B:{blue} | A:{alpha}")]
internal struct MCCVentry
{
    public byte Blue { get; set; }
    public byte Green { get; set; }
    public byte Red { get; set; }
    public byte Alpha { get; set; }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>
        {
            Blue,
            Green,
            Red,
            Alpha
        };

        return bytes.ToArray();
    }
}