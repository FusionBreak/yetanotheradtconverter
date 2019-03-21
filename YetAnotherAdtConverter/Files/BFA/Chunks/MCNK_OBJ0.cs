using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.WOTLK;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MCNK_OBJ0 : Chunk
    {
        int NDoodadRefs = 0;
        int NMapObjRefs = 0;

        MCRD mcrd;
        MCRW mcrw;
        public MCNK_OBJ0(WOTLK.Chunks.MCNK wotlk) : base(wotlk, false)
        {
            NDoodadRefs = (int)wotlk.MCHeader.NDoodadRefs;
            NMapObjRefs = (int)wotlk.MCHeader.NMapObjRefs;

            if(NDoodadRefs > 0)
            {
                UInt32[] doodad = new UInt32[NDoodadRefs];

                for(int x = 0; x < NDoodadRefs; x++)
                {
                    doodad[x] = wotlk.Mcrf.Doodads[x];
                }
                mcrd = new MCRD("MCRD", doodad.Length*4, doodad);
            }

            if (NMapObjRefs > 0)
            {
                UInt32[] doodad = new UInt32[NMapObjRefs];

                for (int x = 0; x < NMapObjRefs; x++)
                {
                    doodad[x] = wotlk.Mcrf.Doodads[x+NDoodadRefs];
                }
                mcrw = new MCRW("MCRW", doodad.Length * 4, doodad);

            }

            Header.ChangeSize(RecalculateSize());
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            if (mcrd != null)
                bytes.AddRange(mcrd.GetBytes());

            if (mcrw != null)
                bytes.AddRange(mcrw.GetBytes());

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            int newSize = 0;

            if (mcrd != null)
                newSize += mcrd.GetBytes().Length;

            if (mcrw != null)
                newSize += mcrw.GetBytes().Length;

            return newSize;
        }
    }

    class MCRD : Chunk
    {
        UInt32[] doodads;
        public MCRD(string magic, int size, UInt32[] content) : base(magic, size, true)
        {
            doodads = content;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (UInt32 x in doodads)
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

    class MCRW : Chunk
    {
        UInt32[] doodads;
        public MCRW(string magic, int size, UInt32[] content) : base(magic, size, true)
        {
            doodads = content;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (UInt32 x in doodads)
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
