using XTI_DB;

namespace XTI_HubDB.EF;

public sealed class HubDbRestore
{
    private readonly HubDbContext hubDbContext;

    public HubDbRestore(HubDbContext hubDbContext)
    {
        this.hubDbContext = hubDbContext;
    }

    public Task Run(string environmentName, string backupFilePath) =>
        new DbRestore(hubDbContext).Run(new HubDbName(environmentName), backupFilePath);
}