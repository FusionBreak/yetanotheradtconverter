using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MLMD : Chunk
{
    private readonly List<lod_object_def> lod_object_defs = new();

    public MLMD(WOTLK.Chunks.MODF wotlk) : base(wotlk, "MLMD")
    {
        foreach (var mapObj in wotlk.MapObjDefs)
        {
            var lodObj = new lod_object_def
            {
                NameID = mapObj.NameID,
                UniqueID = mapObj.UniqueID,
                Position = mapObj.Position,
                Rotation = mapObj.Rotation,
                Flags = mapObj.Flags,
                DoodadSet = mapObj.DoodadSet,
                NameSet = mapObj.NameSet,

                Unk = 1024
            };

            lod_object_defs.Add(lodObj);
        }

        Header.ChangeSize(RecalculateSize());
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in lod_object_defs) bytes.AddRange(x.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        var newSize = 0;

        foreach (var x in lod_object_defs) newSize += x.GetBytes().Length;

        return newSize;
    }
}