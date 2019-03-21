using System;
using System.Collections.Generic;
using System.IO;

namespace YetAnotherAdtConverter
{
    class Program
    {
        /*
         * Unterschiede Legion -> BFA
         * 
         *  _obj1.adt -> MLDX
         *  
         *  .tex -> ? but much
         */
        static public Config config = new Config();
        static void Main(string[] args)
        {

            DirectoryInfo InputDir = new DirectoryInfo(config.InputDir);
            DirectoryInfo OutputDir = new DirectoryInfo(config.OutputDir);

            if (!InputDir.Exists) InputDir.Create(); Logger.log("The input dir was not found and was created.", Logger.Type.WARNING, InputDir.FullName);
            if (!OutputDir.Exists) OutputDir.Create(); Logger.log("The output dir was not found and was created.", Logger.Type.WARNING, OutputDir.FullName);

            //List<Files.WOTLK.ADT> adts = new List<Files.WOTLK.ADT>();
            //List<Files.BFA.ADT> new_adts = new List<Files.BFA.ADT>();
            //List<Files.BFA.OBJ0> new_obj0s = new List<Files.BFA.OBJ0>();
            //List<Files.BFA.OBJ1> new_obj1s = new List<Files.BFA.OBJ1>();
            //List<Files.BFA.TEX0> new_tex0s = new List<Files.BFA.TEX0>();
            foreach (FileInfo file in InputDir.GetFiles())
            {
                if (file.Name.Contains(".adt"))
                {
                    Files.WOTLK.ADT adt = new Files.WOTLK.ADT(file.FullName);
                    new Files.BFA.ADT(adt).WriteFile(OutputDir);
                    new Files.BFA.OBJ0(adt).WriteFile(OutputDir);
                    new Files.BFA.OBJ1(adt).WriteFile(OutputDir);
                    new Files.BFA.TEX0(adt).WriteFile(OutputDir);


                    //adts.Add(new Files.WOTLK.ADT(file.FullName));
                    //new_adts.Add(new Files.BFA.ADT(adts[adts.Count - 1]));
                    //new_obj0s.Add(new Files.BFA.OBJ0(adts[adts.Count - 1]));
                    //new_obj1s.Add(new Files.BFA.OBJ1(adts[adts.Count - 1]));
                    //new_tex0s.Add(new Files.BFA.TEX0(adts[adts.Count - 1]));
                    //new_adts[new_adts.Count - 1].WriteFile(new DirectoryInfo("Output"));
                    //new_obj0s[new_obj0s.Count - 1].WriteFile(new DirectoryInfo("Output"));
                    //new_obj1s[new_obj1s.Count - 1].WriteFile(new DirectoryInfo("Output"));
                    //new_tex0s[new_tex0s.Count - 1].WriteFile(new DirectoryInfo("Output"));
                    Logger.hr();
                }
            }

            Logger.log("Done...", Logger.Type.INFO, "Many thanks to wowdev.wiki. You have done the real work!");
            System.Threading.Thread.Sleep(3000);
        }
    }
}
