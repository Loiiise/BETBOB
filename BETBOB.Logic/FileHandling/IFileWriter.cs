namespace BETBOB.Logic.FileHandling;

public interface IFileWriter
{
    void WriteFile(string filePath, string content, bool overwrite);
    void AppendLineToFile(string filePath, string line, bool createIfNotExists);
}
