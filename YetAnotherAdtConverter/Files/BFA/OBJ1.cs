using System.Diagnostics;
using YetAnotherAdtConverter.Files.BFA.Chunks;

namespace YetAnotherAdtConverter.Files.BFA;

[DebuggerDisplay("{ADTfileInfo.Name}")]
internal class OBJ1
{
    private readonly FileInfo ADTfileInfo;
    public MLDD MLDD;
    public MLDX MLDX;
    public MLFD MLFD;
    public MLMD MLMD;
    public MLMX MLMX;
    public MMDX MMDX;
    public MMID MMID;

    public MVER MVER;
    public MWID MWID;
    public MWMO MWMO;

    public OBJ1(WOTLK.ADT wotlk)
    {
        ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0] + "_obj1.adt");

        MVER = new MVER(wotlk.MVER);
        MLFD = new MLFD(wotlk.MDDF, wotlk.MODF); //I'm not sure the chunk is really needed.
        MMDX = new MMDX(wotlk.MMDX);
        MMID = new MMID(wotlk.MMID);
        MWMO = new MWMO(wotlk.MWMO);
        MWID = new MWID(wotlk.MWID);
        MLDD = new MLDD(wotlk.MDDF);
        MLDX = new MLDX(wotlk.MDDF);
        MLMD = new MLMD(wotlk.MODF);
        MLMX = new MLMX(wotlk.MODF);
    }

    public void WriteFile(DirectoryInfo directory)
    {
        if (!directory.Exists) directory.Create();

        using var writer = new BinaryWriter(File.Open(directory.FullName + "\\" + ADTfileInfo.Name, FileMode.Create));
        writer.Write(MVER.GetBytes());

        if (MLFD != null)
            writer.Write(MLFD.GetBytes());

        writer.Write(MMDX.GetBytes());
        writer.Write(MMID.GetBytes());
        writer.Write(MWMO.GetBytes());
        writer.Write(MWID.GetBytes());
        writer.Write(MLDD.GetBytes());
        writer.Write(MLDX.GetBytes());
        writer.Write(MLMD.GetBytes());
        writer.Write(MLMX.GetBytes());
    }
}