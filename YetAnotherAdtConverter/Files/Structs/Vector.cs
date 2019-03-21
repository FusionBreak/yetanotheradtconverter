using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    /// <summary>
    /// <para>This structure has a fixed size of 12 bytes.</para>
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{x}, {y}, {z}")]
    internal struct Vector
    {
        float x;
        float y;
        float z;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(x));
            bytes.AddRange(BitConverter.GetBytes(y));
            bytes.AddRange(BitConverter.GetBytes(z));

            return bytes.ToArray();
        }
    }
}
