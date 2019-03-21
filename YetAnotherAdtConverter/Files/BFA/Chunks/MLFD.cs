using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    /// <summary> 
    /// I'm not sure the chunk is really needed.
    /// </summary>
    class MLFD : Chunk
    {
        UInt32[] m2LodOffset;  //Index into MLDD per lod
        UInt32[] m2LodLength;  //Number of elements used from MLDD per lod
        UInt32[] wmoLodOffset; //Index into MLMD per lod
        UInt32[] wmoLodLength; //Number of elements used from MLMD per lod

        public MLFD(WOTLK.Chunks.MDDF mddf, WOTLK.Chunks.MODF modf) : base("MLFD", 48, false)
        {
            m2LodOffset = new UInt32[3];
            m2LodLength = new UInt32[3];
            wmoLodOffset = new UInt32[3];
            wmoLodLength = new UInt32[3];


            m2LodOffset[0] = 0;
            m2LodOffset[1] = 0;
            m2LodOffset[2] = 0;

            m2LodLength[0] = (UInt32)mddf.DoodadDefs.Count;// 
            m2LodLength[1] = 0;
            m2LodLength[2] = 0;


            wmoLodOffset[0] = 0;
            wmoLodOffset[1] = 0;
            wmoLodOffset[2] = 0;

            wmoLodLength[0] = 0;
            wmoLodLength[1] = 0;
            wmoLodLength[2] = (UInt32)modf.MapObjDefs.Count;// 
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            List<UInt32> all = new List<UInt32>();
            all.AddRange(m2LodOffset);
            all.AddRange(m2LodLength);
            all.AddRange(wmoLodOffset);
            all.AddRange(wmoLodLength);

            foreach(UInt32 x in all)
            {
                bytes.AddRange(BitConverter.GetBytes(x));
            }

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
