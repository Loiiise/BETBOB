namespace BETBOB.Logic.FileHandling;

public interface IFolderCopyer
{
    public void CopyFolder(string sourceFolder, string desinationFolder, bool overwrite);
}
