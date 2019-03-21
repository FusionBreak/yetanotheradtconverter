using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MDDF : Chunk
    {
        List<DoodadDef> doodadDefs = new List<DoodadDef>();

        public MDDF(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    DoodadDef entry = new DoodadDef();
                    Vector pos = new Vector();
                    Vector rot = new Vector();

                    entry.NameID = reader.ReadUInt32();
                    entry.UniqueID = reader.ReadUInt32();

                    pos.X = reader.ReadSingle();
                    pos.Y = reader.ReadSingle();
                    pos.Z = reader.ReadSingle();
                    entry.Position = pos;

                    rot.X = reader.ReadSingle();
                    rot.Y = reader.ReadSingle();
                    rot.Z = reader.ReadSingle();
                    entry.Rotation = rot;

                    entry.Scale = reader.ReadUInt16();
                    entry.Flags = reader.ReadUInt16();

                    doodadDefs.Add(entry);
                }
            }
        }

        internal List<DoodadDef> DoodadDefs { get => doodadDefs; set => doodadDefs = value; }
    }

    

    

}
