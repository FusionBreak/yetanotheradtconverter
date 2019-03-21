using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.WOTLK;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MTEX : Chunk
    {
        List<string> textures = new List<string>();
        public MTEX(WOTLK.Chunks.MTEX wotlk, bool isSub = false) : base(wotlk, isSub)
        {
            textures = wotlk.Textures;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (string x in textures)
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
