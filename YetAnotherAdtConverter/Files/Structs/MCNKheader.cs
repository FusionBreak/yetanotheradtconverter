namespace YetAnotherAdtConverter.Files.Structs;

internal struct MCNKheader
{
    private offset ofsMCVT;
    private offset ofsMCNR;
    private offset ofsMCLY;
    private offset ofsMCRF;
    private offset ofsMCAL;
    private offset ofsMCSH;
    private offset ofsMCSE;
    private offset ofsMCLQ;
    private offset ofsMCCV;

    public byte[] GetBytes()
    {
        var bytes = new List<byte>();

        bytes.AddRange(BitConverter.GetBytes(Flags));
        bytes.AddRange(BitConverter.GetBytes(IndexX));
        bytes.AddRange(BitConverter.GetBytes(IndexY));
        bytes.AddRange(BitConverter.GetBytes(NLayers));
        bytes.AddRange(BitConverter.GetBytes(NDoodadRefs));

        bytes.AddRange(BitConverter.GetBytes(ofsMCVT.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMCNR.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMCLY.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMCRF.address));
        bytes.AddRange(BitConverter.GetBytes(ofsMCAL.address));

        bytes.AddRange(BitConverter.GetBytes(SizeAlpha));

        bytes.AddRange(BitConverter.GetBytes(ofsMCSH.address));

        bytes.AddRange(BitConverter.GetBytes(SizeShadow));
        bytes.AddRange(BitConverter.GetBytes(Areaid));
        bytes.AddRange(BitConverter.GetBytes(NMapObjRefs));
        bytes.AddRange(BitConverter.GetBytes(Holes));

        bytes.AddRange(GroundEffectsMap);

        bytes.AddRange(BitConverter.GetBytes(PredTex));
        bytes.AddRange(BitConverter.GetBytes(NoEffectDoodad));

        bytes.AddRange(BitConverter.GetBytes(ofsMCSE.address));

        bytes.AddRange(BitConverter.GetBytes(NSndEmitters));

        bytes.AddRange(BitConverter.GetBytes(ofsMCLQ.address));
        bytes.AddRange(BitConverter.GetBytes(SizeLiquid));

        foreach (var x in Pos) bytes.AddRange(BitConverter.GetBytes(x));

        bytes.AddRange(BitConverter.GetBytes(ofsMCCV.address));
        bytes.AddRange(BitConverter.GetBytes(Props));
        bytes.AddRange(BitConverter.GetBytes(EffectId));

        return bytes.ToArray();
    }

    public uint Flags { get; set; }
    public uint IndexX { get; set; }
    public uint IndexY { get; set; }
    public uint NLayers { get; set; }
    public uint NDoodadRefs { get; set; }
    public uint SizeAlpha { get; set; }
    public uint SizeShadow { get; set; }
    public uint Areaid { get; set; }
    public uint NMapObjRefs { get; set; }
    public uint Holes { get; set; }
    public byte[] GroundEffectsMap { get; set; }
    public uint PredTex { get; set; }

    public uint NoEffectDoodad { get; set; }
    public uint NSndEmitters { get; set; }
    public uint SizeLiquid { get; set; }
    public float[] Pos { get; set; }
    public uint Props { get; set; }
    public uint EffectId { get; set; }

    internal offset OfsMCVT
    {
        get => ofsMCVT;
        set => ofsMCVT = value;
    }

    internal offset OfsMCNR
    {
        get => ofsMCNR;
        set => ofsMCNR = value;
    }

    internal offset OfsMCLY
    {
        get => ofsMCLY;
        set => ofsMCLY = value;
    }

    internal offset OfsMCRF
    {
        get => ofsMCRF;
        set => ofsMCRF = value;
    }

    internal offset OfsMCAL
    {
        get => ofsMCAL;
        set => ofsMCAL = value;
    }

    internal offset OfsMCSH
    {
        get => ofsMCSH;
        set => ofsMCSH = value;
    }

    internal offset OfsMCSE
    {
        get => ofsMCSE;
        set => ofsMCSE = value;
    }

    internal offset OfsMCLQ
    {
        get => ofsMCLQ;
        set => ofsMCLQ = value;
    }

    internal offset OfsMCCV
    {
        get => ofsMCCV;
        set => ofsMCCV = value;
    }
}