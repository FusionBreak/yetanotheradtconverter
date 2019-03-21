using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    class MODF : Chunk
    {
        List<mapObjDef> mapObjDefs = new List<mapObjDef>();

        public MODF(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {

            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    mapObjDef entry = new mapObjDef();
                    Vector pos = new Vector();
                    Vector rot = new Vector();
                    CAaBox ext = new CAaBox();
                    Vector min = new Vector();
                    Vector max = new Vector();

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

                    min.X = reader.ReadSingle();
                    min.Y = reader.ReadSingle();
                    min.Z = reader.ReadSingle();
                    max.X = reader.ReadSingle();
                    max.Y = reader.ReadSingle();
                    max.Z = reader.ReadSingle();
                    ext.Min = min;
                    ext.Max = max;
                    entry.Extents = ext;

                    entry.Flags = reader.ReadUInt16();
                    entry.DoodadSet = reader.ReadUInt16();
                    entry.NameSet = reader.ReadUInt16();
                    entry.Scale = reader.ReadUInt16();

                    mapObjDefs.Add(entry);
                }
            }
        }

        internal List<mapObjDef> MapObjDefs { get => mapObjDefs; set => mapObjDefs = value; }
    }

    
}
