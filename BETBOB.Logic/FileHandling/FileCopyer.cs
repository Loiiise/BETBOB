using BETBOB.Logic.Standards;

namespace BETBOB.Logic.FileHandling;

public class FileCopyer : IFileCopyer
{
    public void CopyFile(string sourcePath, string desinationPath, bool overwrite)
    {
        CreateParentFolderIfNotExists(desinationPath);
        File.Copy(sourcePath, desinationPath, overwrite);
    }

    public void CopyFile(FileInfo sourceFile, string desinationPath, bool overwrite)
    {
        CreateParentFolderIfNotExists(desinationPath);
        sourceFile.CopyTo(desinationPath, overwrite);
    }

    private void CreateParentFolderIfNotExists(string path)
    {
        var parentFolder = Path.GetDirectoryName(path);
        if (parentFolder != null)
        {
            SystemsStandards.CreateFolderIfNotExists(parentFolder);
        }
    }
}
