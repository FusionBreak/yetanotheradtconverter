using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    [System.Diagnostics.DebuggerDisplay("{m2s.Count} Files")]
    class MMDX : Chunk
    {
        List<string> m2s = new List<string>();

        public MMDX(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string m2 = "";

                    for (byte x = reader.ReadByte(); x != 0x00; x = reader.ReadByte())
                    {
                        m2 += Convert.ToChar(x);
                    }

                    m2s.Add(m2);
                }
            }
        }

        public List<string> M2s { get => m2s; set => m2s = value; }
    }
}
