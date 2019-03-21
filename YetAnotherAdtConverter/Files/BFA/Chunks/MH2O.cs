using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MH2O : Chunk
    {
        List<MH2O_Header> MH2O_Headers = new List<MH2O_Header>();
        List<data1> data1s = new List<data1>();
        List<data2> data2s = new List<data2>();
        List<byte> rest = new List<byte>();


        int entriesWithLiquid=0; //May not be written to the file!

        public MH2O(Files.WOTLK.Chunks.MH2O wotlk) : base(wotlk)
        {
            MH2O_Headers = wotlk.MH2O_Headers;
            data1s = wotlk.Data1s;
            data2s = wotlk.Data2s;
            rest = wotlk.Rest;
            entriesWithLiquid = wotlk.EntriesWithLiquid;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(Header.GetBytes());
            
            foreach(MH2O_Header x in MH2O_Headers) // 12*256 = 3072 bytes
            {
                bytes.AddRange(x.GetBytes());
            }
            foreach (data1 x in data1s)
            {
                bytes.AddRange(x.GetBytes());
            }
            foreach (data2 x in data2s)
            {
                bytes.AddRange(x.GetBytes());
            }

            bytes.AddRange(rest);

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException(); //Not needed because nothing is changed
        }
    }  
}
