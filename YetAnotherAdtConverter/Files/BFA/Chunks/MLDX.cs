using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MLDX : Chunk
    {
        List<lod_extent> doodadExtents = new List<lod_extent>();

        public MLDX(WOTLK.Chunks.MDDF wotlk) : base(wotlk, "MLDX", false)
        {

            int boundingSize = Program.config.BoundingSize;
            int boundingRadius = Program.config.BoundingRadius;

            if (Program.config.DynamicBoundingGeneration)
            {
                int count = wotlk.DoodadDefs.Count;
                if (wotlk.DoodadDefs.Count > 1000)
                {
                    boundingSize = Convert.ToInt32(boundingSize - ((wotlk.DoodadDefs.Count - 1000) / 10));
                    boundingRadius = Convert.ToInt32(boundingRadius - ((wotlk.DoodadDefs.Count - 1000) / 10));
                    if(boundingSize < 10)
                    {
                        boundingSize = 10;
                        Logger.log("Bounding size was too small. Set to 10", Logger.Type.WARNING);
                    }
                    if (boundingRadius < 10)
                    {
                        boundingRadius = 10;
                        Logger.log("Bounding radius was too small. Set to 10", Logger.Type.WARNING);
                    }
                }                  
            }

            /*
             * doodadDef.Position.X = bounding.Y
             * doodadDef.Position.Y = bounding.Z
             * doodadDef.Position.Z = bounding.X
             */
            foreach (DoodadDef doodadDef in wotlk.DoodadDefs)
            {
                lod_extent extent = new lod_extent();
                CAaBox bounding = new CAaBox();
                Vector min = new Vector();
                Vector max = new Vector();

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
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach (lod_extent x in doodadExtents)
            {
                bytes.AddRange(x.GetBytes());
            }

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            int newSize = 0;

            foreach (lod_extent x in doodadExtents)
            {
                newSize += x.GetBytes().Length;
            }

            return newSize;
        }
    }
}
