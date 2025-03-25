namespace BETBOB.Logic.FileHandling;

public class FileWriter : IFileWriter
{
    public void WriteToFile(string filePath, string content, bool overwrite)
    {
        if (!overwrite && File.Exists(filePath))
        {
            return;
        }

        File.WriteAllText(filePath, content);
    }
}
