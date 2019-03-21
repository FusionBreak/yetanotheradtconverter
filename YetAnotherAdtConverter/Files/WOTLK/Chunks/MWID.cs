using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MWID : Chunk
    {
        List<UInt32> offsets = new List<UInt32>();

        public MWID(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    offsets.Add(reader.ReadUInt32());
                }
            }
        }

        public List<uint> Offsets { get => offsets; set => offsets = value; }
    }
}
