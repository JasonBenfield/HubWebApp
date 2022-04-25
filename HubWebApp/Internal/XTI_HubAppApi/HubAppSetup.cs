using XTI_App.Abstractions;
using XTI_Hub;
namespace XTI_HubAppApi;

public sealed class HubAppSetup : IAppSetup
{
    private readonly AppFactory appFactory;
    private readonly HubAppApiFactory apiFactory;

    public HubAppSetup(AppFactory appFactory, HubAppApiFactory apiFactory)
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