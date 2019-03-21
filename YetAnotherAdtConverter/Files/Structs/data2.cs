using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    /// <summary> The function of this structure did not come to me, so it is simply called "data". ¯\_(ツ)_/¯
    /// <para>This structure has a fixed size of 16 bytes.</para>
    /// </summary>
    struct data2
    {
        byte[] a; //always 8 bytes
        byte[] b; //always 8 bytes

        public byte[] A { get => a; set => a = value; }
        public byte[] B { get => b; set => b = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(a);
            bytes.AddRange(b);

            return bytes.ToArray();
        }
    }
}
