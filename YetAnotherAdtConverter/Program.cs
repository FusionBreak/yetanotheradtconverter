using YetAnotherAdtConverter.Files.BFA;
using ADT = YetAnotherAdtConverter.Files.WOTLK.ADT;

namespace YetAnotherAdtConverter;

internal class Program
{
    /*
     *  Legion -> BFA
     * 
     *  _obj1.adt -> MLDX
     *  
     *  .tex -> ? but much
     */
    public static Config config = new();
    private static readonly GroundEffectsAdder effectsAdder = new();

    private static void Main(string[] args)
    {
        var InputDir = new DirectoryInfo(config.InputDir);
        var OutputDir = new DirectoryInfo(config.OutputDir);

        try
        {
            if (!InputDir.Exists)
            {
                InputDir.Create();
                Console.WriteLine("The input dir was not found and was created.");
            }

            if (!OutputDir.Exists)
            {
                OutputDir.Create();
                Console.WriteLine("The output dir was not found and was created.");
            }

            var files = InputDir.GetFiles().Where(file => file.Extension == ".adt");
            Console.WriteLine($"Start converting: {InputDir.FullName} -> {OutputDir.FullName}");

            _ = Parallel.ForEach(files, file =>
            {
                var wotlk = new ADT(file.FullName);

                if (config.GroundEffectsAdding && !wotlk.ADTfileInfo.Name.Contains("#"))
                    effectsAdder.AddGroundEffects(wotlk);

                new Files.BFA.ADT(wotlk).WriteFile(OutputDir);
                new OBJ0(wotlk).WriteFile(OutputDir);
                new OBJ1(wotlk).WriteFile(OutputDir);
                new TEX0(wotlk).WriteFile(OutputDir);
                Console.WriteLine($"|> {file.Name}");
            });

            Console.WriteLine("Done... Many thanks to wowdev.wiki. You have done the real work!");
            Thread.Sleep(3000);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}