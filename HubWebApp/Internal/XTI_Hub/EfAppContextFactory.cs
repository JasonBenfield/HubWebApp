using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfAppContextFactory
{
    private readonly EfHubDB hubFactory;

    public EfAppContextFactory(EfHubDB hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public EfAppContext Create(AppKey appKey) => new EfAppContext(hubFactory, appKey, AppVersionKey.Current);
}
