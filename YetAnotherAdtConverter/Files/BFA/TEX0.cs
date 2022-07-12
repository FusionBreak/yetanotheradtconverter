using System.Diagnostics;
using YetAnotherAdtConverter.Files.BFA.Chunks;

namespace YetAnotherAdtConverter.Files.BFA;

[DebuggerDisplay("{ADTfileInfo.Name}")]
internal class TEX0
{
    private readonly FileInfo ADTfileInfo;
    private readonly int MCNKLength = 0;
    private readonly List<MCNK_TEX0> MCNKs = new();
    public MAMP MAMP;
    public MTEX MTEX;

    public MVER MVER;

    public TEX0(WOTLK.ADT wotlk)
    {
        ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0] + "_tex0.adt");

        MVER = new MVER(wotlk.MVER);
        MAMP = new MAMP();
        MTEX = new MTEX(wotlk.MTEX);

        foreach (var x in wotlk.MCNKs)
            MCNKs.Add(new MCNK_TEX0(x));
        //MCNKLength += MCNKs[MCNKs.Count - 1].GetBytes().Length;
    }

    public void WriteFile(DirectoryInfo directory)
    {
        if (!directory.Exists) directory.Create();

        using var writer = new BinaryWriter(File.Open(directory.FullName + "\\" + ADTfileInfo.Name, FileMode.Create));
        writer.Write(MVER.GetBytes());
        writer.Write(MAMP.GetBytes());
        writer.Write(MTEX.GetBytes());

        foreach (var x in MCNKs) writer.Write(x.GetBytes());
    }
}