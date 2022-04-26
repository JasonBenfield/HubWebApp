using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_Hub;

public sealed class DefaultAppContext : ISourceAppContext
{
    private readonly AppFactory appFactory;
    private readonly AppKey appKey;
    private readonly AppVersionKey versionKey;

    public DefaultAppContext(AppFactory appFactory, AppKey appKey, AppVersionKey versionKey)
    {
        this.appFactory = appFactory;
        this.appKey = appKey;
        this.versionKey = versionKey;
    }

    public async Task<IApp> App() => await appFactory.Apps.AppOrUnknown(appKey);

    public async Task<ModifierKey> ModKeyInHubApps(IApp app)
    {
        var theApp = await appFactory.Apps.App(app.ID.Value);
        var modKey = await theApp.ModKeyInHubApps();
        return modKey;
    }

    public async Task<IAppVersion> Version()
    {
        var app = await App();
        var version = await app.Version(versionKey);
        return version;
    }
}