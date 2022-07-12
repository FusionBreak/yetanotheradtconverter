using System.Diagnostics;
using YetAnotherAdtConverter.Files.BFA.Chunks;

namespace YetAnotherAdtConverter.Files.BFA;

[DebuggerDisplay("{ADTfileInfo.Name}")]
internal class OBJ0
{
    private readonly FileInfo ADTfileInfo;
    private readonly int MCNKLength;
    private readonly List<MCNK_OBJ0> MCNKs = new();
    public MDDF MDDF;
    public MMDX MMDX;
    public MMID MMID;
    public MODF MODF;

    public MVER MVER;
    public MWID MWID;
    public MWMO MWMO;

    public OBJ0(WOTLK.ADT wotlk)
    {
        ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0] + "_obj0.adt");

        MVER = new MVER(wotlk.MVER);
        MMDX = new MMDX(wotlk.MMDX);
        MMID = new MMID(wotlk.MMID);
        MWMO = new MWMO(wotlk.MWMO);
        MWID = new MWID(wotlk.MWID);
        MDDF = new MDDF(wotlk.MDDF);
        MODF = new MODF(wotlk.MODF);

        foreach (var x in wotlk.MCNKs)
        {
            MCNKs.Add(new MCNK_OBJ0(x));
            MCNKLength += MCNKs[^1].GetBytes().Length;
        }
    }

    public void WriteFile(DirectoryInfo directory)
    {
        if (!directory.Exists) directory.Create();

        using var writer = new BinaryWriter(File.Open(directory.FullName + "\\" + ADTfileInfo.Name, FileMode.Create));
        writer.Write(MVER.GetBytes());
        writer.Write(MMDX.GetBytes());
        writer.Write(MMID.GetBytes());
        writer.Write(MWMO.GetBytes());
        writer.Write(MWID.GetBytes());
        writer.Write(MDDF.GetBytes());
        writer.Write(MODF.GetBytes());

        foreach (var x in MCNKs) writer.Write(x.GetBytes());
    }
}