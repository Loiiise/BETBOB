namespace BETBOB.Logic.FileHandling;

public interface IFileWriter
{
    void WriteToFile(string filePath, string content, bool overwrite);
}
