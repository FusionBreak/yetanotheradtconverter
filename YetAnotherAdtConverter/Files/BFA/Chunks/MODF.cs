using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.WOTLK;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MODF : Chunk
    {
        List<mapObjDef> mapObjDefs = new List<mapObjDef>();
        public MODF(WOTLK.Chunks.MODF wotlk) : base(wotlk, false)
        {
            mapObjDefs = wotlk.MapObjDefs;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (mapObjDef x in mapObjDefs)
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
