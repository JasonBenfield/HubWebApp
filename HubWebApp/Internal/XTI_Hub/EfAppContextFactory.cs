using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class EfAppContextFactory
{
    private readonly EfHubDB db;

    public EfAppContextFactory(EfHubDB db)
    {
        this.db = db;
    }

    public EfAppContext Create(AppKey appKey) => new EfAppContext(db, appKey, AppVersionKey.Current);
}
