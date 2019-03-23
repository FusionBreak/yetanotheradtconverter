using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace YetAnotherAdtConverter
{
    class Config
    {
        public bool LogFile = false;
        public string InputDir = @".\Input"; //Place where the files are read.
        public string OutputDir = @".\Output"; //Place where the files are written.
        public bool CreateMFBO = true; //Should a MFBO chunk be created if the input has none?
        public int BoundingSize = 100;
        public int BoundingRadius = 200;
        public bool DynamicBoundingGeneration = true;

        FileInfo configFile = new FileInfo("config.ini");

        public Config()
        {
            ReadFile();
            
            Logger.log("Configs:", Logger.Type.INFO);
            Logger.log("Use log file: " + LogFile, Logger.Type.LEVEL1);
            Logger.log("Input Directory: " + InputDir, Logger.Type.LEVEL1);
            Logger.log("Output Directory: " + OutputDir, Logger.Type.LEVEL1);
            Logger.log("MFBO creation: " + CreateMFBO, Logger.Type.LEVEL1);
            Logger.log("Bounding size: " + BoundingSize, Logger.Type.LEVEL1);
            Logger.log("Bounding radius: " + BoundingRadius, Logger.Type.LEVEL1);
            Logger.log("Dynamic bounding generation: " + DynamicBoundingGeneration, Logger.Type.LEVEL1);
            Logger.hr();
        }

        void ReadFile()
        {            
            if(!configFile.Exists)
            {
                writeNewFile();
            }

            StreamReader reader = new StreamReader(configFile.FullName);

            for (string line = ""; (line = reader.ReadLine()) != null;)
            {
                string[] setting = line.Split('=');

                switch (setting[0].Trim())
                {
                    case "LogFile":
                        LogFile = Convert.ToBoolean(setting[1].Trim());
                        break;
                    case "Input":
                        InputDir = setting[1].Trim();
                        break;
                    case "Output":
                        OutputDir = setting[1].Trim();
                        break;
                    case "CreateMFBO":
                        CreateMFBO = Convert.ToBoolean(setting[1].Trim());
                        break;
                    case "BoundingSize":
                        BoundingSize = Convert.ToInt32(setting[1].Trim());
                        break;
                    case "BoundingRadius":
                        BoundingRadius = Convert.ToInt32(setting[1].Trim());
                        break;
                    case "DynamicBoundingGeneration":
                        DynamicBoundingGeneration = Convert.ToBoolean(setting[1].Trim());
                        break;
                }
            }
        }

        void writeNewFile()
        {
            Logger.log("Couldn't find any config.ini. A new one was created.", Logger.Type.WARNING);
            string[] lines = {
                @"LogFile=" + LogFile,
                @"Input=" + InputDir,
                @"Output=" + OutputDir,
                @"CreateMFBO=" + CreateMFBO,
                @"BoundingSize=" + BoundingSize,
                @"BoundingRadius=" + BoundingRadius,
                @"DynamicBoundingGeneration=" + DynamicBoundingGeneration
            };

            File.WriteAllLines(configFile.Name, lines);
        }
    }
}
