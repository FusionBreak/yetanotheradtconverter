using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MAMP : Chunk
    {
        byte[] content;
        public MAMP() : base("MAMP", 4)
        {
            content = new byte[4];
            content[0] = 0x0;
            content[1] = 0x0;
            content[2] = 0x0;
            content[3] = 0x0;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(Header.GetBytes());
            bytes.AddRange(content);

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException();
        }
    }
}
