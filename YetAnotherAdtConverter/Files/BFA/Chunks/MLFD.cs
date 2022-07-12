namespace YetAnotherAdtConverter.Files.BFA.Chunks;

/// <summary>
///     I'm not sure the chunk is really needed.
/// </summary>
internal class MLFD : Chunk
{
    private readonly uint[] m2LodLength; //Number of elements used from MLDD per lod
    private readonly uint[] m2LodOffset; //Index into MLDD per lod
    private readonly uint[] wmoLodLength; //Number of elements used from MLMD per lod
    private readonly uint[] wmoLodOffset; //Index into MLMD per lod

    public MLFD(WOTLK.Chunks.MDDF mddf, WOTLK.Chunks.MODF modf) : base("MLFD", 48)
    {
        m2LodOffset = new uint[3];
        m2LodLength = new uint[3];
        wmoLodOffset = new uint[3];
        wmoLodLength = new uint[3];

        m2LodOffset[0] = 0;
        m2LodOffset[1] = 0;
        m2LodOffset[2] = 0;

        m2LodLength[0] = (uint)mddf.DoodadDefs.Count; // 
        m2LodLength[1] = 0;
        m2LodLength[2] = 0;

        wmoLodOffset[0] = 0;
        wmoLodOffset[1] = 0;
        wmoLodOffset[2] = 0;

        wmoLodLength[0] = 0;
        wmoLodLength[1] = 0;
        wmoLodLength[2] = (uint)modf.MapObjDefs.Count; // 
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        var all = new List<uint>();
        all.AddRange(m2LodOffset);
        all.AddRange(m2LodLength);
        all.AddRange(wmoLodOffset);
        all.AddRange(wmoLodLength);

        foreach (var x in all) bytes.AddRange(BitConverter.GetBytes(x));

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        throw new NotImplementedException();
    }
}