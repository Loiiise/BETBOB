
namespace BETBOB.Logic.FileHandling;

public class FileReader : IFileReader
{
    public string ReadFile(string path) => File.ReadAllText(path);

    public IEnumerable<string> ReadLines(string path)
    {
        if (!Path.Exists(path))
        {
            throw new FileNotFoundException(path);
        }

        StreamReader streamReader = new StreamReader(path);
        while (streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();

            if (line != null) yield return line;
        }
        streamReader.Close();
    }
}
