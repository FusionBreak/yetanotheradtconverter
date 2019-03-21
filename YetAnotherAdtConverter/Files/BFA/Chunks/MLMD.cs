using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MLMD : Chunk
    {
        List<lod_object_def> lod_object_defs = new List<lod_object_def>();

        public MLMD(WOTLK.Chunks.MODF wotlk) : base(wotlk, "MLMD", false)
        {
            foreach(mapObjDef mapObj in wotlk.MapObjDefs)
            {
                lod_object_def lodObj = new lod_object_def();
                lodObj.NameID = mapObj.NameID;
                lodObj.UniqueID = mapObj.UniqueID;
                lodObj.Position = mapObj.Position;
                lodObj.Rotation = mapObj.Rotation;
                lodObj.Flags = mapObj.Flags;
                lodObj.DoodadSet = mapObj.DoodadSet;
                lodObj.NameSet = mapObj.NameSet;

                lodObj.Unk = 1024;

                lod_object_defs.Add(lodObj);
            }

            Header.ChangeSize(RecalculateSize());
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (lod_object_def x in lod_object_defs)
            {
                bytes.AddRange(x.GetBytes());
            }

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            int newSize = 0;

            foreach (lod_object_def x in lod_object_defs)
            {
                newSize += x.GetBytes().Length;
            }

            return newSize;
        }
    }
}
