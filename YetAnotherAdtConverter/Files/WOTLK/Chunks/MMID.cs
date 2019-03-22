using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MMID : Chunk
    {
        List<UInt32> offsets = new List<UInt32>();

        public MMID(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    offsets.Add(BitConverter.ToUInt32(reader.ReadBytes(4),0));
                }
            }
        }

        public List<UInt32> Offsets { get => offsets; set => offsets = value; }
    }
}
