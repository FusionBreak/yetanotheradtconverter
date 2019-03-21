using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    struct Plane
    {
        Int16[][] height; // = [3][3]

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            for (int x=0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    bytes.AddRange(BitConverter.GetBytes(height[x][y])); //
                }
            }
            return bytes.ToArray();
        }

        public short[][] Height { get => height; set => height = value; }
    }
}
