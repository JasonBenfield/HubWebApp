using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_HubAppApi;

namespace XTI_HubSetup;

public sealed class HubAppSetup : IAppSetup
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;
    private readonly HubAppApiFactory apiFactory;
    private readonly VersionReader versionReader;
    private readonly string domain;

    public HubAppSetup(AppFactory appFactory, IClock clock, HubAppApiFactory apiFactory, VersionReader versionReader, string domain)
    {
        this.appFactory = appFactory;
        this.clock = clock;
        this.apiFactory = apiFactory;
        this.versionReader = versionReader;
        this.domain = domain;
    }

    public async Task Run(AppVersionKey versionKey)
    {
        var versions = await versionReader.Versions();
        var template = apiFactory.CreateTemplate();
        var registration = new AppRegistration(appFactory, clock);
        await registration.Run(template.ToModel(), domain, versionKey, versions);
    }
}