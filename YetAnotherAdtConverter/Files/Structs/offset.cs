using System.Diagnostics;

namespace YetAnotherAdtConverter.Files.Structs;

[DebuggerDisplay("{address}")]
internal struct offset
{
    public uint address;
}