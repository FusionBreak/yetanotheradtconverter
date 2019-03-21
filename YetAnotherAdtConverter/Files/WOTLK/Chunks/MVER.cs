using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks
{
    //[System.Diagnostics.DebuggerDisplay("Version: {version}")]
    class MVER : Chunk
    {
        public byte[] version;
        public MVER(char[] magic, byte[] size, byte[] content) : base(magic, size)
        {
            version = content;
        }
    }
}
