using YetAnotherAdtConverter.Files.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    [System.Diagnostics.DebuggerDisplay("flags: {flags}")]
    class MHDR : Chunk
    {
        UInt32 flags;

        offset ofsMCIN;
        offset ofsMTEX;
        offset ofsMMDX;
        offset ofsMMID;
        offset ofsMWMO;
        offset ofsMWID;
        offset ofsMDDF;
        offset ofsMODF;
        offset ofsMFBO;
        offset ofsMH2O;
        offset ofsMTFX;
        offset pad4;
        offset pad5;
        offset pad6;
        offset pad7;

        public MHDR(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(content)))
            {
                flags = reader.ReadUInt32();
                ofsMCIN.address = reader.ReadUInt32();
                ofsMTEX.address = reader.ReadUInt32();
                ofsMMDX.address = reader.ReadUInt32();
                ofsMMID.address = reader.ReadUInt32();
                ofsMWMO.address = reader.ReadUInt32();
                ofsMWID.address = reader.ReadUInt32();
                ofsMDDF.address = reader.ReadUInt32();
                ofsMODF.address = reader.ReadUInt32();
                ofsMFBO.address = reader.ReadUInt32();
                ofsMH2O.address = reader.ReadUInt32();
                ofsMTFX.address = reader.ReadUInt32();
                pad4.address = reader.ReadUInt32();
                pad5.address = reader.ReadUInt32();
                pad6.address = reader.ReadUInt32();
                pad7.address = reader.ReadUInt32();
            }
        }

        public uint Flags { get => flags; set => flags = value; }
        internal offset OfsMCIN { get => ofsMCIN; set => ofsMCIN = value; }
        internal offset OfsMTEX { get => ofsMTEX; set => ofsMTEX = value; }
        internal offset OfsMMDX { get => ofsMMDX; set => ofsMMDX = value; }
        internal offset OfsMMID { get => ofsMMID; set => ofsMMID = value; }
        internal offset OfsMWMO { get => ofsMWMO; set => ofsMWMO = value; }
        internal offset OfsMWID { get => ofsMWID; set => ofsMWID = value; }
        internal offset OfsMDDF { get => ofsMDDF; set => ofsMDDF = value; }
        internal offset OfsMODF { get => ofsMODF; set => ofsMODF = value; }
        internal offset OfsMFBO { get => ofsMFBO; set => ofsMFBO = value; }
        internal offset OfsMH2O { get => ofsMH2O; set => ofsMH2O = value; }
        internal offset OfsMTFX { get => ofsMTFX; set => ofsMTFX = value; }
        internal offset Pad4 { get => pad4; set => pad4 = value; }
        internal offset Pad5 { get => pad5; set => pad5 = value; }
        internal offset Pad6 { get => pad6; set => pad6 = value; }
        internal offset Pad7 { get => pad7; set => pad7 = value; }
    }

    
}
