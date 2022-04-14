using XTI_DB;

namespace XTI_HubDB.EF;

public sealed class HubDbBackup
{
    private readonly HubDbContext hubDbContext;

    public HubDbBackup(HubDbContext hubDbContext)
    {
        this.hubDbContext = hubDbContext;
    }

    public Task Run(string environmentName, string backupFilePath)
        => new DbBackup(hubDbContext).Run(new HubDbName(environmentName), backupFilePath);
}