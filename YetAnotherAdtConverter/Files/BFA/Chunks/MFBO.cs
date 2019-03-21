using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MFBO : Chunk
    {
        Plane maximum;
        Plane minimum;

        public MFBO(Files.WOTLK.Chunks.MFBO wotlk) : base(wotlk)
        {
            maximum = wotlk.Maximum;
            minimum = wotlk.Minimum;
        }

        public MFBO() : base("MFBO", 36)
        {
            maximum.Height = new Int16[3][];
            minimum.Height = new Int16[3][];
            for (int x = 0; x < 3; x++)
            {
                maximum.Height[x] = new Int16[3];
                minimum.Height[x] = new Int16[3];
                for (int y = 0; y < 3; y++)
                {
                    maximum.Height[x][y] = (Int16)900;
                    minimum.Height[x][y] = (Int16)(-0x96);
                }
            }
        }

        internal Plane Maximum { get => maximum; set => maximum = value; }
        internal Plane Minimum { get => minimum; set => minimum = value; }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(Header.GetBytes());

            bytes.AddRange(maximum.GetBytes());
            bytes.AddRange(minimum.GetBytes());

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
