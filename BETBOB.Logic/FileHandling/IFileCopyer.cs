namespace BETBOB.Logic.FileHandling;

public interface IFileCopyer
{
    public void CopyFile(FileInfo sourceFile, string desinationPath, bool overwrite);
    public void CopyFile(string sourcePath, string desinationPath, bool overwrite);
}
