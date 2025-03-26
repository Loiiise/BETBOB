namespace BETBOB.Logic.Standards;

internal static class ProgramStandards
{
    internal const string ProgramName = "BETBOB";
    internal const string DefaultConfigurationFileName = $"{ProgramName}.config";

    private const string TmpFolderName = "tmp";

    internal static string TemporaryFolderLocation()
    {
        var appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Join(appdataFolder, ProgramName, TmpFolderName);
    }

    internal static string ZipArchiveTemporaryFolderName(string subfolderName)
    {
        var subfolder = Path.Join(TemporaryFolderLocation(), subfolderName);

        SystemsStandards.CreateFolderIfNotExists(subfolder);
        return subfolder;
    }
}
