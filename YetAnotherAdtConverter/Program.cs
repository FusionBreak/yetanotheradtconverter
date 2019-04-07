using System;
using System.Collections.Generic;
using System.IO;

namespace YetAnotherAdtConverter
{
    class Program
    {
        /*
         *  Legion -> BFA
         * 
         *  _obj1.adt -> MLDX
         *  
         *  .tex -> ? but much
         */
        static public Config config = new Config();
        static GroundEffectsAdder effectsAdder = new GroundEffectsAdder();
        static void Main(string[] args)
        {
            DirectoryInfo InputDir = new DirectoryInfo(config.InputDir);
            DirectoryInfo OutputDir = new DirectoryInfo(config.OutputDir);

            try
            {
                
                if (!InputDir.Exists) { InputDir.Create(); Logger.log("The input dir was not found and was created.", Logger.Type.WARNING, InputDir.FullName); }
                if (!OutputDir.Exists) { OutputDir.Create(); Logger.log("The output dir was not found and was created.", Logger.Type.WARNING, OutputDir.FullName); }
                foreach (FileInfo file in InputDir.GetFiles())
                {
                    if (file.Name.Contains(".adt"))
                    {
                        Files.WOTLK.ADT adt = new Files.WOTLK.ADT(file.FullName);

                        if(config.GroundEffectsAdding && !adt.ADTfileInfo.Name.Contains("#"))
                        {
                            adt = effectsAdder.AddGroundEffects(adt);
                        }
                        new Files.BFA.ADT(adt).WriteFile(OutputDir);
                        new Files.BFA.OBJ0(adt).WriteFile(OutputDir);
                        new Files.BFA.OBJ1(adt).WriteFile(OutputDir);
                        new Files.BFA.TEX0(adt).WriteFile(OutputDir);
                        Logger.hr();
                    }
                }

                Logger.log("Done...", Logger.Type.INFO, "Many thanks to wowdev.wiki. You have done the real work!");
                System.Threading.Thread.Sleep(3000);
            }
            catch(Exception e)
            {
                Logger.log(e.Message, Logger.Type.ERROR, e.StackTrace);
            }            
        }
    }
}
