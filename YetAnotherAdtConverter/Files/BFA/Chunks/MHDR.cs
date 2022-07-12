using System.Diagnostics;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

// a MHDR Chunk is always 64 Byte large
[DebuggerDisplay("flags: {flags}")]
internal class MHDR : Chunk
{
    private offset ofsMCIN; // 1 = contains a MFBO chunk. | 2 = some northrend stuff.
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

    public MHDR(WOTLK.Chunks.MHDR wotlk) : base(wotlk)
    {
        Flags = 0;

        ofsMCIN.address = 0;
        ofsMTEX.address = 0;
        ofsMMDX.address = 0;
        ofsMMID.address = 0;
        ofsMWMO.address = 0;
        ofsMWID.address = 0;
        ofsMDDF.address = 0;
        ofsMODF.address = 0;

        ofsMFBO.address = 0; //OfsMH2O + MH20 size

        ofsMH2O = wotlk.OfsMH2O; //
        ofsMTFX.address = 0;
        pad4.address = 0;
        pad5.address = 0;
        pad6.address = 0;
        pad7.address = 0;
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

    public void UpdateMFBO(uint newAddress)
    {
        Flags = 1;
        ofsMFBO.address = newAddress;
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(Header.GetBytes());

        bytes.AddRange(BitConverter.GetBytes(Flags));
        bytes.AddRange(BitConverter.GetBytes(ofsMCIN.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMTEX.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMMDX.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMMID.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMWMO.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMWID.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMDDF.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMODF.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMFBO.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMH2O.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMTFX.address));
        bytes.AddRange(BitConverter.GetBytes(pad4.address));
        bytes.AddRange(BitConverter.GetBytes(pad5.address));
        bytes.AddRange(BitConverter.GetBytes(pad6.address));
        bytes.AddRange(BitConverter.GetBytes(pad7.address));

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
        //Not needed, because fixed size : 64 byte
    }
}