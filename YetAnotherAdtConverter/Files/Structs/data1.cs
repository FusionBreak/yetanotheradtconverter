using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    /// <summary> The function of this structure did not come to me, so it is simply called "data". ¯\_(ツ)_/¯
    /// <para>This structure has a fixed size of 24 bytes.</para>
    /// </summary>
    struct data1
    {
        UInt16 flags;
        UInt16 type;
        Single height1;
        Single height2;
        byte xoff;
        byte yoff;
        byte w;
        byte h;
        UInt32 of2a;
        UInt32 of2b;

        public UInt16 Flags { get => flags; set => flags = value; }
        public UInt16 Type { get => type; set => type = value; }
        public Single Height1 { get => height1; set => height1 = value; }
        public Single Height2 { get => height2; set => height2 = value; }
        public byte Xoff { get => xoff; set => xoff = value; }
        public byte Yoff { get => yoff; set => yoff = value; }
        public byte W { get => w; set => w = value; }
        public byte H { get => h; set => h = value; }
        public UInt32 Of2a { get => of2a; set => of2a = value; }
        public UInt32 Of2b { get => of2b; set => of2b = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(Flags));
            bytes.AddRange(BitConverter.GetBytes(Type));
            bytes.AddRange(BitConverter.GetBytes(Height1));
            bytes.AddRange(BitConverter.GetBytes(Height2));

            bytes.Add(Xoff);
            bytes.Add(Yoff);
            bytes.Add(W);
            bytes.Add(H);

            bytes.AddRange(BitConverter.GetBytes(Of2a));
            bytes.AddRange(BitConverter.GetBytes(Of2b));

            return bytes.ToArray();
        }
    }
}
