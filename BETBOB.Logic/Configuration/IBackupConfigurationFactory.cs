namespace BETBOB.Logic.Configuration;

public interface IBackupConfigurationFactory
{
    BackupConfiguration Default();
    string DefaultString();
    BackupConfiguration FromJson(string json);
    string ToJson(BackupConfiguration configuration);
}
