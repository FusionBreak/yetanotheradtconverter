using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    [System.Diagnostics.DebuggerDisplay("{wmos.Count} Files")]
    class MWMO : Chunk
    {
        List<string> wmos = new List<string>();

        public MWMO(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string wmo = "";

                    for (byte x = reader.ReadByte(); x != 0x00; x = reader.ReadByte())
                    {
                        wmo += Convert.ToChar(x);
                    }

                    wmos.Add(wmo);
                }
            }
        }

        public List<string> Wmos { get => wmos; set => wmos = value; }
    }
}
