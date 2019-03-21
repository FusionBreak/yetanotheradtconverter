using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter
{
    class Config
    {
        public string InputDir = @".\Input"; //Place where the files are read.
        public string OutputDir = @".\Output"; //Place where the files are written.

        public bool CreateMFBO = true; //Should a MFBO chunk be created if the input has none?

        public int BoundingSize = 100;
        public int BoundingRadius = 200;
        public bool DynamicBoundingGeneration = true;

        public Config()
        {
            Logger.log("Configs:", Logger.Type.INFO);
            Logger.log("Input Directory: " + InputDir, Logger.Type.LEVEL1);
            Logger.log("Output Directory: " + OutputDir, Logger.Type.LEVEL1);
            Logger.log("MFBO creation: " + CreateMFBO, Logger.Type.LEVEL1);
            Logger.log("Bounding size: " + BoundingSize, Logger.Type.LEVEL1);
            Logger.log("Bounding radius: " + BoundingRadius, Logger.Type.LEVEL1);
            Logger.log("Dynamic bounding generation: " + DynamicBoundingGeneration, Logger.Type.LEVEL1);
            Logger.hr();
        }

        public bool ReadFile()
        {
            return false;
        }
    }
}
