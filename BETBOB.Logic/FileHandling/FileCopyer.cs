namespace BETBOB.Logic.FileHandling;

public class FileCopyer : IFileCopyer
{
    public void CopyFile(string sourcePath, string desinationPath, bool overwrite)
        => File.Copy(sourcePath, desinationPath, overwrite);

    public void CopyFile(FileInfo sourceFile, string desinationPath, bool overwrite)
        => sourceFile.CopyTo(desinationPath, overwrite);
}
