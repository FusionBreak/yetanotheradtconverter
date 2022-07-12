using System.Diagnostics;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.WOTLK.Chunks;

[DebuggerDisplay("flags: {flags}")]
internal class MHDR : Chunk
{
    private offset ofsMCIN;
    private offset ofsMDDF;
    private offset ofsMFBO;
    private offset ofsMH2O;
    private offset ofsMMDX;
    private offset ofsMMID;
    private offset ofsMODF;
    private offset ofsMTEX;
    private offset ofsMTFX;
    private offset ofsMWID;
    private offset ofsMWMO;
    private offset pad4;
    private offset pad5;
    private offset pad6;
    private offset pad7;

    public MHDR(char[] magic, byte[] size, byte[] content) : base(magic, size)
    {
        using var reader = new BinaryReader(new MemoryStream(content));
        Flags = reader.ReadUInt32();
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

    public uint Flags { get; set; }

    internal offset OfsMCIN
    {
        get => ofsMCIN;
        set => ofsMCIN = value;
    }

    internal offset OfsMTEX
    {
        get => ofsMTEX;
        set => ofsMTEX = value;
    }

    internal offset OfsMMDX
    {
        get => ofsMMDX;
        set => ofsMMDX = value;
    }

    internal offset OfsMMID
    {
        get => ofsMMID;
        set => ofsMMID = value;
    }

    internal offset OfsMWMO
    {
        get => ofsMWMO;
        set => ofsMWMO = value;
    }

    internal offset OfsMWID
    {
        get => ofsMWID;
        set => ofsMWID = value;
    }

    internal offset OfsMDDF
    {
        get => ofsMDDF;
        set => ofsMDDF = value;
    }

    internal offset OfsMODF
    {
        get => ofsMODF;
        set => ofsMODF = value;
    }

    internal offset OfsMFBO
    {
        get => ofsMFBO;
        set => ofsMFBO = value;
    }

    internal offset OfsMH2O
    {
        get => ofsMH2O;
        set => ofsMH2O = value;
    }

    internal offset OfsMTFX
    {
        get => ofsMTFX;
        set => ofsMTFX = value;
    }

    internal offset Pad4
    {
        get => pad4;
        set => pad4 = value;
    }

    internal offset Pad5
    {
        get => pad5;
        set => pad5 = value;
    }

    internal offset Pad6
    {
        get => pad6;
        set => pad6 = value;
    }

    internal offset Pad7
    {
        get => pad7;
        set => pad7 = value;
    }
}