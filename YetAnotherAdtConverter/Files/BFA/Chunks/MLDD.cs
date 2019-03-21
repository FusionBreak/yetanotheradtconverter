using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.Structs;
using YetAnotherAdtConverter.Files.WOTLK;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MLDD : Chunk
    {
        List<DoodadDef> doodadDefs = new List<DoodadDef>();

        public MLDD(WOTLK.Chunks.MDDF wotlk) : base(wotlk, "MLDD", false)
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
