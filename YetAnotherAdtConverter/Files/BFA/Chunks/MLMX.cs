using System;
using System.Collections.Generic;
using YetAnotherAdtConverter.Files.Structs;

namespace YetAnotherAdtConverter.Files.BFA.Chunks
{
    class MLMX : Chunk
    {
        List<lod_extent> objectExtents = new List<lod_extent>();

        public MLMX(WOTLK.Chunks.MODF wotlk) : base(wotlk, "MLMX", false)
        {
            int boundingSize = Program.config.BoundingSize;
            int boundingRadius = Program.config.BoundingRadius;

            if(Program.config.DynamicBoundingGeneration)
            {
                int count = wotlk.MapObjDefs.Count;
                if(wotlk.MapObjDefs.Count > 1000)
                {
                    boundingSize = Convert.ToInt32(boundingSize - ((wotlk.MapObjDefs.Count - 1000) / 10));
                    boundingRadius = Convert.ToInt32(boundingRadius - ((wotlk.MapObjDefs.Count - 1000) / 10));
                    if(boundingSize < 10)
                    {
                        boundingSize = 10;
                    }
                    if(boundingRadius < 10)
                    {
                        boundingRadius = 10;
                    }
                }
            }

            /*
             * objDef.Position.X = bounding.Y
             * objDef.Position.Y = bounding.Z
             * objDef.Position.Z = bounding.X
             */
            foreach(mapObjDef objDef in wotlk.MapObjDefs)
            {
                lod_extent extent = new lod_extent();
                CAaBox bounding = new CAaBox();
                Vector min = new Vector();
                Vector max = new Vector();

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
            List<byte> bytes = new List<byte>();
            bytes.AddRange(Header.GetBytes());

            foreach(lod_extent x in objectExtents)
            {
                bytes.AddRange(x.GetBytes());
            }

            return bytes.ToArray();
        }

        public override int RecalculateSize()
        {
            int newSize = 0;

            foreach(lod_extent x in objectExtents)
            {
                newSize += x.GetBytes().Length;
            }

            return newSize;
        }
    }
}
