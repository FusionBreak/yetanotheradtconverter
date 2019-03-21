using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    /// <summary> Only found in an MCNR chunk. Always an array of 145 (9*9 + 8*8) elements.
    /// <para>This structure has a fixed size of 3 bytes.</para>
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("X:{x} | Y:{y} | Z:{z}")]
    struct MCNRentry
    {
        Byte x;
        Byte z;
        Byte y;

        public byte X { get => x; set => x = value; }
        public byte Z { get => z; set => z = value; }
        public byte Y { get => y; set => y = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.Add(x);
            bytes.Add(z);
            bytes.Add(y);

            return bytes.ToArray();
        }
    }
}
