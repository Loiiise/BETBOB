using System.Reflection;
using static System.Environment;

namespace BETBOB.Logic.Standards;

internal static class SystemsStandards
{
    internal const string DefaultDriveRoot => "C:";
    internal static string? GetExecutableLocation() => Assembly.GetEntryAssembly() is Assembly assembly ? Path.GetDirectoryName(assembly.Location) : null;

    internal static string[] GetCommonFolders()
        => new SpecialFolder[]
        {
            SpecialFolder.Desktop,
            SpecialFolder.MyDocuments,
            SpecialFolder.Personal,
            SpecialFolder.Favorites,
            SpecialFolder.MyMusic,
            SpecialFolder.MyVideos,
            SpecialFolder.DesktopDirectory,
            SpecialFolder.MyPictures,
            SpecialFolder.CommonDocuments,
            SpecialFolder.CommonMusic,
            SpecialFolder.CommonPictures,
            SpecialFolder.CommonVideos,
        }
        .Select(GetFolderPath)
        // SpecialFolder may point to the same folder on certain systems
        .Distinct()
        .ToArray();

    internal static string[] GetAllFolders()
        => new SpecialFolder[] {
            SpecialFolder.Desktop,
            SpecialFolder.Programs,
            SpecialFolder.MyDocuments,
            SpecialFolder.Personal,
            SpecialFolder.Favorites,
            SpecialFolder.Startup,
            SpecialFolder.Recent,
            SpecialFolder.SendTo,
            SpecialFolder.StartMenu,
            SpecialFolder.MyMusic,
            SpecialFolder.MyVideos,
            SpecialFolder.DesktopDirectory,
            SpecialFolder.MyComputer,
            SpecialFolder.NetworkShortcuts,
            SpecialFolder.Fonts,
            SpecialFolder.Templates,
            SpecialFolder.CommonStartMenu,
            SpecialFolder.CommonPrograms,
            SpecialFolder.CommonStartup,
            SpecialFolder.CommonDesktopDirectory,
            SpecialFolder.ApplicationData,
            SpecialFolder.PrinterShortcuts,
            SpecialFolder.LocalApplicationData,
            SpecialFolder.InternetCache,
            SpecialFolder.Cookies,
            SpecialFolder.History,
            SpecialFolder.CommonApplicationData,
            SpecialFolder.Windows,
            SpecialFolder.System,
            SpecialFolder.ProgramFiles,
            SpecialFolder.MyPictures,
            SpecialFolder.UserProfile,
            SpecialFolder.SystemX86,
            SpecialFolder.ProgramFilesX86,
            SpecialFolder.CommonProgramFiles,
            SpecialFolder.CommonProgramFilesX86,
            SpecialFolder.CommonTemplates,
            SpecialFolder.CommonDocuments,
            SpecialFolder.CommonAdminTools,
            SpecialFolder.AdminTools,
            SpecialFolder.CommonMusic,
            SpecialFolder.CommonPictures,
            SpecialFolder.CommonVideos,
            SpecialFolder.Resources,
            SpecialFolder.LocalizedResources,
            SpecialFolder.CommonOemLinks,
            SpecialFolder.CDBurning,
        }
        .Select(GetFolderPath)
        // SpecialFolder may point to the same folder on certain systems
        .Distinct()
        .ToArray();
}
