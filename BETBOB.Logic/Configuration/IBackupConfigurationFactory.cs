namespace BETBOB.Logic.Configuration;

public interface IBackupConfigurationFactory
{
    BackupConfiguration Default();
    BackupConfiguration FromJson(string json);
    string ToJson(BackupConfiguration configuration);
}
