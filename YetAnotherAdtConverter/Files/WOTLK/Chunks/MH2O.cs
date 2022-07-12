using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

internal class MH2O : Chunk
{
    public MH2O(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));

        for (var x = 0; x < 256; x++)
        {
            var hdr = new MH2O_Header
            {
                Offset_attributes = reader.ReadUInt32(),
                Layer_count = reader.ReadUInt32(),
                Offset_instances = reader.ReadUInt32()
            };

            MH2O_Headers.Add(hdr);

            if (hdr.Layer_count > 0)
                EntriesWithLiquid++;
        }

        for (var x = 0; x < EntriesWithLiquid; x++)
        {
            var data = new data1
            {
                Flags = reader.ReadUInt16(),
                Type = reader.ReadUInt16(),
                Height1 = reader.ReadSingle(),
                Height2 = reader.ReadSingle(),
                Xoff = reader.ReadByte(),
                Yoff = reader.ReadByte(),
                W = reader.ReadByte(),
                H = reader.ReadByte(),
                Of2a = reader.ReadUInt32(),
                Of2b = reader.ReadUInt32()
            };

            Data1s.Add(data);
        }

        for (var x = 0; x < EntriesWithLiquid; x++)
        {
            var data = new data2
            {
                A = reader.ReadBytes(8),
                B = reader.ReadBytes(8)
            };

            Data2s.Add(data);
        }

        while (reader.BaseStream.Position < reader.BaseStream.Length)
            Rest.Add(reader
                .ReadByte()); //The remaining bytes we just leave bytes. I don't understand them and don't have to change them, lul.
    }

    public List<byte> Rest { get; set; } = new();
    public int EntriesWithLiquid { get; set; }
    public List<MH2O_Header> MH2O_Headers { get; set; } = new();
    public List<data1> Data1s { get; set; } = new();
    public List<data2> Data2s { get; set; } = new();
}