namespace XTI_HubAppApi;

public sealed class HubAppSetup : IAppSetup
{
    private readonly HubFactory appFactory;
    private readonly HubAppApiFactory apiFactory;

    public HubAppSetup(HubFactory appFactory, HubAppApiFactory apiFactory)
    {
        this.appFactory = appFactory;
        this.apiFactory = apiFactory;
    }

    public async Task Run(AppVersionKey versionKey)
    {
        var template = apiFactory.CreateTemplate();
        var registration = new AppRegistration(appFactory);
        await registration.Run(template.ToModel(), versionKey);
    }
}