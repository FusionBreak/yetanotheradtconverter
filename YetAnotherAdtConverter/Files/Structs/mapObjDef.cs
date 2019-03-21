using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{

    ///<summary>
    ///<para>Placement information for WMOs. Additional to this, the WMOs to render are referenced in each MCRF chunk.</para>
    ///</summary>
    [System.Diagnostics.DebuggerDisplay("uniqueID: {uniqueID}")]
    struct mapObjDef
    {
        UInt32 nameID;
        UInt32 uniqueID;
        Vector position;
        Vector rotation;
        CAaBox extents;
        UInt16 flags;
        UInt16 doodadSet;
        UInt16 nameSet;
        UInt16 scale;

        public UInt32 NameID { get => nameID; set => nameID = value; }
        public UInt32 UniqueID { get => uniqueID; set => uniqueID = value; }
        public UInt16 Scale { get => scale; set => scale = value; }
        public UInt16 Flags { get => flags; set => flags = value; }
        public UInt16 DoodadSet { get => doodadSet; set => doodadSet = value; }
        public UInt16 NameSet { get => nameSet; set => nameSet = value; }
        internal Vector Position { get => position; set => position = value; }
        internal Vector Rotation { get => rotation; set => rotation = value; }
        internal CAaBox Extents { get => extents; set => extents = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(nameID));
            bytes.AddRange(BitConverter.GetBytes(uniqueID));
            bytes.AddRange(position.GetBytes());
            bytes.AddRange(rotation.GetBytes());
            bytes.AddRange(extents.GetBytes());
            bytes.AddRange(BitConverter.GetBytes(flags));
            bytes.AddRange(BitConverter.GetBytes(doodadSet));
            bytes.AddRange(BitConverter.GetBytes(nameSet));
            bytes.AddRange(BitConverter.GetBytes(scale));

            return bytes.ToArray();
        }
    }
}
