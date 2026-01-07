using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Fakes;

namespace HubWebApp.Fakes;

public sealed class FakeHubSetup : IAppSetup
{
    private readonly AppApiFactory apiFactory;
    private readonly FakeAppContext appContext;

    public FakeHubSetup(AppApiFactory apiFactory, FakeAppContext appContext)
    {
        this.apiFactory = apiFactory;
        this.appContext = appContext;
    }

    public async Task Run(AppVersionKey versionKey, CancellationToken ct)
    {
        var setup = new DefaultFakeSetup(apiFactory, appContext);
        await setup.Run(versionKey, ct);
        appContext.SetCurrentApp(setup.App);
    }
}