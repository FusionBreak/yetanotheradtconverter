using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    [System.Diagnostics.DebuggerDisplay("Min: {min.X}, {min.Y}, {min.Z} | Max: {max.X}, {max.Y}, {max.Z}")]
    struct CAaBox
    {
        Vector min;
        Vector max;

        internal Vector Min { get => min; set => min = value; }
        internal Vector Max { get => max; set => max = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(min.GetBytes());
            bytes.AddRange(max.GetBytes());

            return bytes.ToArray();
        }
    }
}
