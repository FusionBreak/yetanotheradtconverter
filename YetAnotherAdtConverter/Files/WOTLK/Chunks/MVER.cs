namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

//[System.Diagnostics.DebuggerDisplay("Version: {version}")]
internal class MVER : Chunk
{
    public byte[] version;

    public MVER(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        version = content;
    }
}