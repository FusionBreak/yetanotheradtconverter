using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MCIN : Chunk
    {
        List<Entry> entries = new List<Entry>();

        public MCIN(char[] magic, byte[] size, byte[] content): base(magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Entry entry = new Entry();

                    
                    entry.adress = reader.ReadUInt32();
                    entry.size = reader.ReadUInt32();
                    entry.flags = reader.ReadUInt32();
                    entry.asyncID = reader.ReadUInt32();

                    entries.Add(entry);
                }           
            }
        }
    }
}
