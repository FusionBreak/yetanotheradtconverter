using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK
{
    [System.Diagnostics.DebuggerDisplay("{Header.Byte_size} byte")]
    class Chunk
    {
        public ChunkHeader Header;
        
        public Chunk(char[] magic, byte[] size, bool isSub = false)
        {
            Header = new ChunkHeader(magic, size);

            if(Header.GetHeaderString() != "MCNK" && !isSub)
            {
                    Logger.log(Header.GetHeaderString(), Logger.Type.LEVEL1, Header.Byte_size + " byte");
            }            
        }
    }

    class ChunkHeader
    {
        public char[] Magic;
        public int Byte_size = 0;
        public byte[] Size;

        public ChunkHeader(char[] magic, byte[] size)
        {
            Magic = magic;
            Size = size;
            Byte_size = BitConverter.ToInt32(size, 0);
            //Array.Reverse(Magic);
        }

        public string GetHeaderString()
        {
            return new string(Magic);
        }
    }
}
