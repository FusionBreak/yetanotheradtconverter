using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                if(!InputDir.Exists) { InputDir.Create(); Console.WriteLine("The input dir was not found and was created."); }
                if(!OutputDir.Exists) { OutputDir.Create(); Console.WriteLine("The output dir was not found and was created."); }

                var files = InputDir.GetFiles().Where(file => file.Extension == ".adt");
                Console.WriteLine($"Start converting: {InputDir.FullName} -> {OutputDir.FullName}");

                Parallel.ForEach(files, file =>
                {
                    var wotlk = new Files.WOTLK.ADT(file.FullName);

                    if(config.GroundEffectsAdding && !wotlk.ADTfileInfo.Name.Contains("#"))
                    {
                        effectsAdder.AddGroundEffects(wotlk);
                    }

                    new Files.BFA.ADT(wotlk).WriteFile(OutputDir);
                    new Files.BFA.OBJ0(wotlk).WriteFile(OutputDir);
                    new Files.BFA.OBJ1(wotlk).WriteFile(OutputDir);
                    new Files.BFA.TEX0(wotlk).WriteFile(OutputDir);
                    Console.WriteLine($"|> {file.Name}");
                });


                Console.WriteLine("Done... Many thanks to wowdev.wiki. You have done the real work!");
                System.Threading.Thread.Sleep(3000);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
