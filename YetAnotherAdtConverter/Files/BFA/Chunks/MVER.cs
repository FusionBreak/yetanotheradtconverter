using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    //[System.Diagnostics.DebuggerDisplay("Version: {version}")]
    class MVER : Chunk
    {
        byte[] version;
        public MVER(Files.WOTLK.Chunks.MVER wotlk) : base(wotlk)
        {
            version = wotlk.version;
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(Header.GetBytes());
            bytes.AddRange(version);

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            throw new NotImplementedException(); //Not needed, because fixed size : 4 byte
        }
    }
}
