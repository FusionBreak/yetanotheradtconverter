using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.WOTLK;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MWMO : Chunk
    {
        List<string> wmos = new List<string>();
        public MWMO(WOTLK.Chunks.MWMO wotlk) : base(wotlk, false)
        {
            wmos = wotlk.Wmos;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (string x in wmos)
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
