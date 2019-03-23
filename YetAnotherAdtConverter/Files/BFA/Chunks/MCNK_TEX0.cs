using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.WOTLK;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MCNK_TEX0 : Chunk
    {
        MCLY mcly;
        MCSH mcsh;
        MCAL mcal;
        public MCNK_TEX0(WOTLK.Chunks.MCNK wotlk) : base(wotlk, false)
        {
            if (wotlk.Mcly != null)
                mcly = new MCLY(wotlk.Mcly);

            if (wotlk.Mcsh != null)
                mcsh = new MCSH(wotlk.Mcsh);

            if (wotlk.Mcal != null)
                mcal = new MCAL(wotlk.Mcal);

            Header.ChangeSize(RecalculateSize());
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            if (mcly != null)
                bytes.AddRange(mcly.GetBytes());

            if (mcsh != null)
                bytes.AddRange(mcsh.GetBytes());

            if (mcal != null)
                bytes.AddRange(mcal.GetBytes());

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            int newSize = 0;

            if (mcly != null)
                newSize += mcly.GetBytes().Length;

            if (mcsh != null)
                newSize += mcsh.GetBytes().Length;

            if (mcal != null)
                newSize += mcal.GetBytes().Length;

            return newSize;
        }
    }

    class MCLY : Chunk
    {
        List<MCLYentry> entries;

        public MCLY(Files.WOTLK.Chunks.MCLY wotlk) : base(wotlk, true)
        {
            entries = wotlk.Entries;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (MCLYentry x in entries)
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

    class MCSH : Chunk
    {
        byte[] data;

        public MCSH(Files.WOTLK.Chunks.MCSH wotlk) : base(wotlk, true)
        {
            data = wotlk.Data;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            bytes.AddRange(data);

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException();
        }
    }

    class MCAL : Chunk
    {
        byte[] data;

        public MCAL(Files.WOTLK.Chunks.MCAL wotlk) : base(wotlk, true)
        {
            data = wotlk.Data;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            bytes.AddRange(data);

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
