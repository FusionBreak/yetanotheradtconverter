using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    [System.Diagnostics.DebuggerDisplay("TextureID: {textid}")]
    struct MCLYentry
    {
        UInt32 textid;
        UInt32 flags;
        UInt32 ofsalphamap;
        UInt32 detailtextureid;

        public uint Textid { get => textid; set => textid = value; }
        public uint Flags { get => flags; set => flags = value; }
        public uint Ofsalphamap { get => ofsalphamap; set => ofsalphamap = value; }
        public uint Detailtextureid { get => detailtextureid; set => detailtextureid = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(textid));
            bytes.AddRange(BitConverter.GetBytes(flags));
            bytes.AddRange(BitConverter.GetBytes(ofsalphamap));
            bytes.AddRange(BitConverter.GetBytes(detailtextureid));

            return bytes.ToArray();
        }
    }
}
