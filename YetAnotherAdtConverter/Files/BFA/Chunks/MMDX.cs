using YetAnotherAdtConverter.Files.Structs;
using YetAnotherAdtConverter.Files.WOTLK;
using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MMDX : Chunk
    {
        List<string> m2s = new List<string>();
        public MMDX(WOTLK.Chunks.MMDX wotlk) : base(wotlk, false)
        {
            m2s = wotlk.M2s;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (string x in m2s)
            {
                bytes.AddRange(Encoding.ASCII.GetBytes(x));
                bytes.Add(0x0);
            }

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
