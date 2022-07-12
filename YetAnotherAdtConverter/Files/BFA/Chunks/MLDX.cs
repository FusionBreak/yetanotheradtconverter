using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks;

internal class MLDX : Chunk
{
    private readonly List<lod_extent> doodadExtents = new();

    public MLDX(WOTLK.Chunks.MDDF wotlk) : base(wotlk, "MLDX")
    {
        var boundingSize = Program.config.BoundingSize;
        var boundingRadius = Program.config.BoundingRadius;

        if (Program.config.DynamicBoundingGeneration)
        {
            _ = wotlk.DoodadDefs.Count;
            if (wotlk.DoodadDefs.Count > 1000)
            {
                boundingSize = Convert.ToInt32(boundingSize - (wotlk.DoodadDefs.Count - 1000) / 10);
                boundingRadius = Convert.ToInt32(boundingRadius - (wotlk.DoodadDefs.Count - 1000) / 10);
                if (boundingSize < 10) boundingSize = 10;

                if (boundingRadius < 10) boundingRadius = 10;
            }
        }

        /*
         * doodadDef.Position.X = bounding.Y
         * doodadDef.Position.Y = bounding.Z
         * doodadDef.Position.Z = bounding.X
         */
        foreach (var doodadDef in wotlk.DoodadDefs)
        {
            var extent = new lod_extent();
            var bounding = new CAaBox();
            var min = new Vector();
            var max = new Vector();

            min.X = ConvertClientCoordsToServerCoords(doodadDef.Position.Z) - boundingSize;
            min.Y = ConvertClientCoordsToServerCoords(doodadDef.Position.X) - boundingSize;
            min.Z = doodadDef.Position.Y - boundingSize;

            max.X = ConvertClientCoordsToServerCoords(doodadDef.Position.Z) + boundingSize;
            max.Y = ConvertClientCoordsToServerCoords(doodadDef.Position.X) + boundingSize;
            max.Z = doodadDef.Position.Y + boundingSize;

            bounding.Min = min;
            bounding.Max = max;
            extent.Bounding = bounding;
            extent.Radius = boundingRadius;

            doodadExtents.Add(extent);
        }

        Header.ChangeSize(RecalculateSize());
    }

    public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(Header.GetBytes());

        foreach (var x in doodadExtents) bytes.AddRange(x.GetBytes());

        return bytes.ToArray();
    }

    public override int RecalculateSize()
    {
        var newSize = 0;

        foreach (var x in doodadExtents) newSize += x.GetBytes().Length;

        return newSize;
    }
}