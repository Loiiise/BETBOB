namespace BETBOB.Logic.FileHandling;

public interface IFileReader
{
    string ReadFile(string path);
    IEnumerable<string> ReadLines(string path);
}
