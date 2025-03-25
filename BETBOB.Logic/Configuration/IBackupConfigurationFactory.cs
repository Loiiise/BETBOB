namespace BETBOB.Logic.Configuration;

public interface IBackupConfigurationFactory
{
    BackupConfiguration Empty();
    BackupConfiguration FromJson(string json);
    string ToJson(BackupConfiguration configuration);
}
