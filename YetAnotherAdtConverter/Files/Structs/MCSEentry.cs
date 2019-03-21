using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    /// <summary> Only found in an MCSE chunk. Always an array of 145 (9*9 + 8*8) elements.
    /// <para>This structure has a fixed size of 3 bytes.</para>
    /// </summary>
    struct MCSEentry
    {
        UInt32 dbcidd;
        Vector position;
        Vector radius;

        public uint Dbcidd { get => dbcidd; set => dbcidd = value; }
        internal Vector Position { get => position; set => position = value; }
        internal Vector Radius { get => radius; set => radius = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(dbcidd));
            bytes.AddRange(position.GetBytes());
            bytes.AddRange(radius.GetBytes());

            return bytes.ToArray();
        }
    }
}
