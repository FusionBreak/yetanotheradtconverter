using System.Diagnostics;
using YetAnotherAdtConverter.Files.WOTLK.Chunks;

namespace YetAnotherAdtConverter.Files.WOTLK;

[DebuggerDisplay("{ADTfileInfo.Name} | {Length/1024}KB")]
internal class ADT
{
    private readonly int Length;
    public FileInfo ADTfileInfo;
    public MCIN MCIN;
    public List<MCNK> MCNKs = new();
    public MDDF MDDF;
    public MFBO MFBO;
    public MH2O MH2O;
    public MHDR MHDR;
    public MMDX MMDX;
    public MMID MMID;
    public MODF MODF;
    public MTEX MTEX;

    public MVER MVER;
    public MWID MWID;
    public MWMO MWMO;

    public ADT(string filePath)
    {
        ADTfileInfo = new FileInfo(filePath);
        //Start reading the .ADT File
        using var reader = new BinaryReader(ADTfileInfo.Open(FileMode.Open));
        Length = (int)reader.BaseStream.Length;
        _ = reader.BaseStream.Seek(0, SeekOrigin.Begin);
        var MCNK_counter = 0;
        var MCNK_size = 0;

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            var ChunkMagic = reader.ReadBytes(4);
            var ChunkSize = reader.ReadBytes(4);
            var ChunkContent = reader.ReadBytes(BitConverter.ToInt32(ChunkSize, 0));

            var ChunkMagicString = MagicBytesToString(ChunkMagic);
            //read the chunks
            switch (ChunkMagicString)
            {
                case "MVER":
                    MVER = new MVER(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MHDR":
                    MHDR = new MHDR(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCIN":
                    MCIN = new MCIN(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MTEX":
                    MTEX = new MTEX(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MMDX":
                    MMDX = new MMDX(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MMID":
                    MMID = new MMID(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MWMO":
                    MWMO = new MWMO(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MWID":
                    MWID = new MWID(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MDDF":
                    MDDF = new MDDF(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MODF":
                    MODF = new MODF(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MH2O":
                    MH2O = new MH2O(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
                case "MCNK":
                    MCNKs.Add(new MCNK(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent));
                    break;
                case "MFBO":
                    MFBO = new MFBO(MagicBytesToChars(ChunkMagic), ChunkSize, ChunkContent);
                    break;
            }

            if (ChunkMagicString == "MCNK")
            {
                MCNK_counter++;
                MCNK_size += BitConverter.ToInt32(ChunkSize, 0);
            }
            else if (ChunkMagicString == "\0\0\0\0")
            {
                /*Logger.log("0 Byte Chunk", Logger.Direction.WARNING);*/
            }
        }
    }

    public static char[] MagicBytesToChars(byte[] MagicBytes)
    {
        char[] MagicChars;
        MagicChars = new char[MagicBytes.Length];

        var count = 0;
        foreach (var MagicByte in MagicBytes)
        {
            MagicChars[count] = Convert.ToChar(MagicByte);
            count++;
        }

        Array.Reverse(MagicChars);

        return MagicChars;
    }

    public static string MagicBytesToString(byte[] MagicBytes)
    {
        return new(MagicBytesToChars(MagicBytes));
    }
}