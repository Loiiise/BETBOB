namespace BETBOB.Logic.FileHandling;

public class FolderCopyer : IFolderCopyer
{
    public FolderCopyer(IFileCopyer fileCopyer)
    {
        _fileCopyer = fileCopyer;
    }

    public void CopyFolder(string sourceFolder, string desinationFolder, bool overwrite)
    {
        var sourceDirectory = new DirectoryInfo(sourceFolder);
        if (!sourceDirectory.Exists) throw new DirectoryNotFoundException(sourceDirectory.FullName);

        // Cache directories before we start copying
        DirectoryInfo[] sourceSubDirectories = sourceDirectory.GetDirectories();
        Directory.CreateDirectory(desinationFolder);

        // Copy files
        foreach (FileInfo sourceFile in sourceDirectory.GetFiles())
        {
            string destinationFilePath = Path.Combine(desinationFolder, sourceFile.Name);
            _fileCopyer.CopyFile(sourceFile, destinationFilePath, overwrite);
        }

        // Recursively copy subfolders 
        foreach (DirectoryInfo subDirectory in sourceSubDirectories)
        {
            string newDestinationDirectory = Path.Combine(desinationFolder, subDirectory.Name);
            CopyFolder(subDirectory.FullName, newDestinationDirectory, overwrite);
        }
    }

    private readonly IFileCopyer _fileCopyer;
}
