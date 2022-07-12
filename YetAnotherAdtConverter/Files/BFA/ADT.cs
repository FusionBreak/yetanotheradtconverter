using System.Diagnostics;
using YetAnotherAdtConverter.Files.BFA.Chunks;

namespace YetAnotherAdtConverter.Files.BFA;

[DebuggerDisplay("{ADTfileInfo.Name}")]
internal class ADT
{
    private readonly FileInfo ADTfileInfo;
    private readonly int MCNKLength;
    private readonly List<MCNK_ADT> MCNKs = new();
    public MFBO MFBO;
    public MH2O MH2O;
    public MHDR MHDR;

    public MVER MVER;

    public ADT(WOTLK.ADT wotlk)
    {
        ADTfileInfo = new FileInfo(wotlk.ADTfileInfo.Name.Split('.')[0] + ".adt");

        MVER = new MVER(wotlk.MVER);
        MHDR = new MHDR(wotlk.MHDR);

        if (wotlk.MH2O != null)
            MH2O = new MH2O(wotlk.MH2O);

        foreach (var x in wotlk.MCNKs)
        {
            MCNKs.Add(new MCNK_ADT(x));
            MCNKLength += MCNKs[^1].GetBytes().Length;
        }

        if (wotlk.MFBO != null)
        {
            MFBO = new MFBO(wotlk.MFBO);
        }
        else if (Program.config.CreateMFBO)
        {
            uint address = 0;
            MFBO = new MFBO();

            address += (uint)MHDR.Header.Byte_size;

            if (MH2O != null) address += (uint)MH2O.GetBytes().Length;

            address += (uint)MCNKLength;
            MHDR.UpdateMFBO(address);
        }
    }

    public void WriteFile(DirectoryInfo directory)
    {
        if (!directory.Exists) directory.Create();

        using var writer = new BinaryWriter(File.Open(directory.FullName + "\\" + ADTfileInfo.Name, FileMode.Create));
        writer.Write(MVER.GetBytes());
        writer.Write(MHDR.GetBytes());

        if (MH2O != null)
            writer.Write(MH2O.GetBytes());

        foreach (var x in MCNKs) writer.Write(x.GetBytes());

        if (MFBO != null)
            writer.Write(MFBO.GetBytes());
    }
}