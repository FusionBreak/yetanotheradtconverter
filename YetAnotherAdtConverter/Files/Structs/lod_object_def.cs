using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    struct lod_object_def
    {
        UInt32 nameID;
        UInt32 uniqueID;
        Vector position;
        Vector rotation;
        UInt16 flags;
        UInt16 doodadSet;
        UInt16 nameSet;
        UInt16 unk;

        public uint NameID { get => nameID; set => nameID = value; }
        public uint UniqueID { get => uniqueID; set => uniqueID = value; }
        public ushort Flags { get => flags; set => flags = value; }
        public ushort DoodadSet { get => doodadSet; set => doodadSet = value; }
        public ushort NameSet { get => nameSet; set => nameSet = value; }
        public ushort Unk { get => unk; set => unk = value; }
        internal Vector Position { get => position; set => position = value; }
        internal Vector Rotation { get => rotation; set => rotation = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(nameID));
            bytes.AddRange(BitConverter.GetBytes(uniqueID));
            bytes.AddRange(position.GetBytes());
            bytes.AddRange(rotation.GetBytes());
            bytes.AddRange(BitConverter.GetBytes(flags));
            bytes.AddRange(BitConverter.GetBytes(doodadSet));
            bytes.AddRange(BitConverter.GetBytes(nameSet));
            bytes.AddRange(BitConverter.GetBytes(unk));

            return bytes.ToArray();
        }
    }
}
