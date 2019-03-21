using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MH2O : Chunk
    {
        List<MH2O_Header> mH2O_Headers = new List<MH2O_Header>();
        List<data1> data1s = new List<data1>();
        List<data2> data2s = new List<data2>();
        List<byte> rest = new List<byte>();
        int entriesWithLiquid=0;

        public MH2O(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {   
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {

                for (int x = 0; x < 256;x++)
                {
                    MH2O_Header hdr = new MH2O_Header();
                    hdr.Offset_attributes = reader.ReadUInt32();
                    hdr.Layer_count = reader.ReadUInt32();
                    hdr.Offset_instances = reader.ReadUInt32();

                    mH2O_Headers.Add(hdr);

                    if (hdr.Layer_count > 0)
                        entriesWithLiquid++;
                }
                
                for(int x = 0; x < entriesWithLiquid; x++)
                {
                    data1 data = new data1();
                    data.Flags = reader.ReadUInt16();
                    data.Type = reader.ReadUInt16();
                    data.Height1 = reader.ReadSingle();
                    data.Height2 = reader.ReadSingle();
                    data.Xoff = reader.ReadByte();
                    data.Yoff = reader.ReadByte();
                    data.W = reader.ReadByte();
                    data.H = reader.ReadByte();
                    data.Of2a = reader.ReadUInt32();
                    data.Of2b = reader.ReadUInt32();

                    data1s.Add(data);
                }

                for (int x = 0; x < entriesWithLiquid; x++)
                {
                    data2 data = new data2();
                    data.A = reader.ReadBytes(8);
                    data.B = reader.ReadBytes(8);

                    data2s.Add(data);
                }

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    rest.Add(reader.ReadByte()); //The remaining bytes we just leave bytes. I don't understand them and don't have to change them, lul.
                }
            }
        }

        public List<byte> Rest { get => rest; set => rest = value; }
        public int EntriesWithLiquid { get => entriesWithLiquid; set => entriesWithLiquid = value; }
        public List<MH2O_Header> MH2O_Headers { get => mH2O_Headers; set => mH2O_Headers = value; }
        public List<data1> Data1s { get => data1s; set => data1s = value; }
        public List<data2> Data2s { get => data2s; set => data2s = value; }
    }  
}
