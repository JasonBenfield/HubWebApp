using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class EfAppContextFactory
{
    private readonly HubFactory hubFactory;

    public EfAppContextFactory(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public EfAppContext Create(AppKey appKey) => new EfAppContext(hubFactory, appKey, AppVersionKey.Current);
}
