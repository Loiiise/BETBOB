namespace BETBOB.Logic.Standards;

internal static class ProgramStandards
{
    internal const string ProgramName = "BETBOB";
    internal const string DefaultConfigurationFileName = $"{ProgramName}.config";

    private const string ZipTmpFolder = "tmp";
    internal static string ZipArchiveTemporaryFolderName(string subfolderName)
    {
        var appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var subfolder = Path.Join(appdataFolder, ProgramName, ZipTmpFolder, subfolderName);

        SystemsStandards.CreateFolderIfNotExists(subfolder);
        return subfolder;
    }
}
