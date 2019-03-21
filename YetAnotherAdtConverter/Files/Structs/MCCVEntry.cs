using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    /// <summary> Only found in an MCCV chunk. Always an array of 145 (9*9 + 8*8) elements.
    /// <para>This structure has a fixed size of 4 bytes.</para>
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("R:{red} | G:{green} | B:{blue} | A:{alpha}")]
    struct MCCVentry
    {
        byte blue;
        byte green;
        byte red;
        byte alpha;

        public byte Blue { get => blue; set => blue = value; }
        public byte Green { get => green; set => green = value; }
        public byte Red { get => red; set => red = value; }
        public byte Alpha { get => alpha; set => alpha = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.Add(blue);
            bytes.Add(green);
            bytes.Add(red);
            bytes.Add(alpha);

            return bytes.ToArray();
        }
    }
}
