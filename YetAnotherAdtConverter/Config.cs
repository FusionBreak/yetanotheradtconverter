namespace YetAnotherAdtConverter;

internal class Config
{
    private readonly FileInfo configFile = new("config.ini");
    public int BoundingRadius = 200;
    public int BoundingSize = 100;
    public bool CreateMFBO = true; //Should a MFBO chunk be created if the input has none?
    public bool DynamicBoundingGeneration = true;
    public bool GroundEffectsAdding;
    public string InputDir = @".\Input"; //Place where the files are read.
    public bool LogFile;
    public string OutputDir = @".\Output"; //Place where the files are written.
    public bool ReCalculateGroundEffectsMap;

    public Config()
    {
        ReadFile();
    }

    private void ReadFile()
    {
        if (!configFile.Exists) writeNewFile();

        var reader = new StreamReader(configFile.FullName);
        string? line;
        for (; (line = reader.ReadLine()) != null;)
        {
            var setting = line.Split('=');

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
                case "GroundEffectsAdding":
                    GroundEffectsAdding = Convert.ToBoolean(setting[1].Trim());
                    break;
                case "ReCalculateGroundEffectsMap":
                    ReCalculateGroundEffectsMap = Convert.ToBoolean(setting[1].Trim());
                    break;
            }
        }
    }

    private void writeNewFile()
    {
        string[] lines =
        {
            @"LogFile=" + LogFile,
            @"Input=" + InputDir,
            @"Output=" + OutputDir,
            @"CreateMFBO=" + CreateMFBO,
            @"BoundingSize=" + BoundingSize,
            @"BoundingRadius=" + BoundingRadius,
            @"DynamicBoundingGeneration=" + DynamicBoundingGeneration,
            @"GroundEffectsAdding=" + GroundEffectsAdding,
            @"ReCalculateGroundEffectsMap=" + ReCalculateGroundEffectsMap
        };

        File.WriteAllLines(configFile.Name, lines);
    }
}