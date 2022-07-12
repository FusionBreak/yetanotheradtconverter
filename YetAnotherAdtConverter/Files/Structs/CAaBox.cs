using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

[DebuggerDisplay("Min: {min.X}, {min.Y}, {min.Z} | Max: {max.X}, {max.Y}, {max.Z}")]
internal struct CAaBox
{
    private Vector min;
    private Vector max;

    internal Vector Min
    {
        get => min;
        set => min = value;
    }

    internal Vector Max
    {
        get => max;
        set => max = value;
    }

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(min.GetBytes());
        bytes.AddRange(max.GetBytes());

        return bytes.ToArray();
    }
}