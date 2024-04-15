using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;

namespace XTI_AppSetupApp.Extensions;

public sealed class DefaultAppSetup : IAppSetup
{
    private readonly HubAppClient hubClient;
    private readonly SystemUserCredentials systemUserCredentials;
    private readonly AppApiFactory apiFactory;
    private readonly AppVersionName versionName;
    private AppModel? app;
    private string? modKey;

    public DefaultAppSetup(HubAppClient hubClient, AppApiFactory apiFactory, SystemUserCredentials systemUserCredentials, SetupOptions options)
    {
        this.hubClient = hubClient;
        this.apiFactory = apiFactory;
        this.systemUserCredentials = systemUserCredentials;
        versionName = new AppVersionName(options.VersionName);
    }

    public AppModel App
    {
        get => app ?? throw new ArgumentNullException(nameof(app));
    }

    public string ModKey
    {
        get => modKey ?? throw new ArgumentNullException(nameof(modKey));
    }

    public async Task Run(AppVersionKey versionKey)
    {
        var template = apiFactory.CreateTemplate();
        await hubClient.Install.AddOrUpdateApps
        (
            new AddOrUpdateAppsRequest
            (
                versionName: versionName,
                appKeys: [template.AppKey]
            )
        );
        var password = Guid.NewGuid().ToString();
        var systemUser = await hubClient.Install.AddSystemUser
        (
            new AddSystemUserRequest
            (
                appKey: template.AppKey,
                machineName: Environment.MachineName,
                password: password
            )
        );
        await systemUserCredentials.Update
        (
            new CredentialValue
            (
                systemUser.UserName.Value,
                password
            )
        );
        app = await hubClient.Install.RegisterApp
        (
            new RegisterAppRequest
            (
                appTemplate: template.ToModel(),
                versionKey: versionKey
            )
        );
        modKey = app.PublicKey;
    }
}