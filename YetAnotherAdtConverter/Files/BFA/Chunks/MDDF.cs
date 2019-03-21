using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.WOTLK;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MDDF : Chunk
    {
        List<DoodadDef> doodadDefs = new List<DoodadDef>();
        public MDDF(WOTLK.Chunks.MDDF wotlk) : base(wotlk, false)
        {
            doodadDefs = wotlk.DoodadDefs;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (DoodadDef x in doodadDefs)
            {
                bytes.AddRange(x.GetBytes());
            }

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
