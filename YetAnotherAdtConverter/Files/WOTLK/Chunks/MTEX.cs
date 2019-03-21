using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    [System.Diagnostics.DebuggerDisplay("{textures.Count} Files")]
    class MTEX : Chunk
    {
        List<string> textures = new List<string>();

        public MTEX(char[] magic, byte[] size, byte[] content) : base(magic,size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {               
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string texture = "";

                    for(byte x = reader.ReadByte(); x != 0x00; x = reader.ReadByte())
                    {
                        texture += Convert.ToChar(x);                       
                    }

                    textures.Add(texture);
                }                
            }            
        }

        public List<string> Textures { get => textures; set => textures = value; }
    }
}
