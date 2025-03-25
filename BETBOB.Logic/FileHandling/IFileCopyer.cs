namespace BETBOB.Logic.FileHandling;

internal interface IFileCopyer
{
    public void CopyFile(string sourcePath, string desinationPath, bool overwrite);
}
