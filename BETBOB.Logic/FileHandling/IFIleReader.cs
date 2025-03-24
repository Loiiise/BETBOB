namespace BETBOB.Logic.FileHandling;

public interface IFIleReader
{
    string ReadFile(string path);
    IEnumerable<string> ReadLines(string path);
}
