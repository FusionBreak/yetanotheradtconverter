using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MFBO : Chunk
    {
        Plane maximum;
        Plane minimum;

        public MFBO(char[] magic, byte[] size, byte[] content) : base(magic, size, false)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                maximum.Height = new Int16[3][];
                for (int x = 0; x < 3; x++)
                {
                    maximum.Height[x] = new Int16[3];
                    for (int y = 0; y < 3; y++)
                    {
                        maximum.Height[x][y] = reader.ReadInt16();
                    }
                }

                minimum.Height = new Int16[3][];
                for (int x = 0; x < 3; x++)
                {
                    minimum.Height[x] = new Int16[3];
                    for (int y = 0; y < 3; y++)
                    {
                        minimum.Height[x][y] = reader.ReadInt16();
                    }
                }
            }
        }

        internal Plane Maximum { get => maximum; set => maximum = value; }
        internal Plane Minimum { get => minimum; set => minimum = value; }
    }
}
