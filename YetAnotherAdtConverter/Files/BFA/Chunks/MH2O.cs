using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MH2O : Chunk
{
    private readonly List<data1> data1s = new();
    private readonly List<data2> data2s = new();
    private readonly int entriesWithLiquid; //May not be written to the file!
    private readonly List<MH2O_Header> MH2O_Headers = new();
    private readonly List<byte> rest = new();

    public MH2O(WOTLK.Chunks.MH2O wotlk) : base(wotlk)
    {
        MH2O_Headers = wotlk.MH2O_Headers;
        data1s = wotlk.Data1s;
        data2s = wotlk.Data2s;
        rest = wotlk.Rest;
        entriesWithLiquid = wotlk.EntriesWithLiquid;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        foreach (var x in MH2O_Headers) // 12*256 = 3072 bytes
            bytes.AddRange(x.GetBytes());

        foreach (var x in data1s) bytes.AddRange(x.GetBytes());

        foreach (var x in data2s) bytes.AddRange(x.GetBytes());

        bytes.AddRange(rest);

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
        //Not needed because nothing is changed
    }
}