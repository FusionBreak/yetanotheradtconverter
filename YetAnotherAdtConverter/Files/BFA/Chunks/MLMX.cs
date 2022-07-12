using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MLMX : Chunk
{
    private readonly List<lod_extent> objectExtents = new();

    public MLMX(WOTLK.Chunks.MODF wotlk) : base(wotlk, "MLMX")
    {
        var boundingSize = Program.config.BoundingSize;
        var boundingRadius = Program.config.BoundingRadius;

        if (Program.config.DynamicBoundingGeneration)
        {
            _ = wotlk.MapObjDefs.Count;
            if (wotlk.MapObjDefs.Count > 1000)
            {
                boundingSize = Convert.ToInt32(boundingSize - (wotlk.MapObjDefs.Count - 1000) / 10);
                boundingRadius = Convert.ToInt32(boundingRadius - (wotlk.MapObjDefs.Count - 1000) / 10);
                if (boundingSize < 10) boundingSize = 10;

                if (boundingRadius < 10) boundingRadius = 10;
            }
        }

        /*
         * objDef.Position.X = bounding.Y
         * objDef.Position.Y = bounding.Z
         * objDef.Position.Z = bounding.X
         */
        foreach (var objDef in wotlk.MapObjDefs)
        {
            var extent = new lod_extent();
            var bounding = new CAaBox();
            var min = new Vector();
            var max = new Vector();

            min.X = ConvertClientCoordsToServerCoords(objDef.Position.Z) - boundingSize;
            min.Y = ConvertClientCoordsToServerCoords(objDef.Position.X) - boundingSize;
            min.Z = objDef.Position.Y - boundingSize;

            max.X = ConvertClientCoordsToServerCoords(objDef.Position.Z) + boundingSize;
            max.Y = ConvertClientCoordsToServerCoords(objDef.Position.X) + boundingSize;
            max.Z = objDef.Position.Y + boundingSize;

            bounding.Min = min;
            bounding.Max = max;
            extent.Bounding = bounding;
            extent.Radius = boundingRadius;

            objectExtents.Add(extent);
        }

        Header.ChangeSize(RecalculateSize());
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in objectExtents) bytes.AddRange(x.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        var newSize = 0;

        foreach (var x in objectExtents) newSize += x.GetBytes().Length;

        return newSize;
    }
}