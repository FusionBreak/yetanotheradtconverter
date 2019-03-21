using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    struct lod_extent
    {
        CAaBox bounding;
        Single radius;

        public Single Radius { get => radius; set => radius = value; }
        internal CAaBox Bounding { get => bounding; set => bounding = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(bounding.GetBytes());
            bytes.AddRange(BitConverter.GetBytes(radius));

            return bytes.ToArray();
        }
    }
}
