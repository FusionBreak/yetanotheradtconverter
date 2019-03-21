using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    [System.Diagnostics.DebuggerDisplay("uniqueID: {uniqueID}")]
    struct DoodadDef
    {
        UInt32 nameID;
        UInt32 uniqueID;
        Vector position;
        Vector rotation;
        UInt16 scale;
        UInt16 flags;

        public UInt32 NameID { get => nameID; set => nameID = value; }
        public UInt32 UniqueID { get => uniqueID; set => uniqueID = value; }
        public UInt16 Scale { get => scale; set => scale = value; }
        public UInt16 Flags { get => flags; set => flags = value; }
        internal Vector Position { get => position; set => position = value; }
        internal Vector Rotation { get => rotation; set => rotation = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(nameID));
            bytes.AddRange(BitConverter.GetBytes(uniqueID));
            bytes.AddRange(position.GetBytes());
            bytes.AddRange(rotation.GetBytes());
            bytes.AddRange(BitConverter.GetBytes(scale));
            bytes.AddRange(BitConverter.GetBytes(flags));

            return bytes.ToArray();
        }
    }
}
