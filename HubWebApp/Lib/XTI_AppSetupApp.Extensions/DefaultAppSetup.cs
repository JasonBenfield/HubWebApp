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
            {
                VersionName = versionName,
                Apps = new[] { new AppDefinitionModel(template.AppKey) }
            }
        );
        var password = Guid.NewGuid().ToString();
        var systemUser = await hubClient.Install.AddSystemUser
        (
            new AddSystemUserRequest
            {
                AppKey = template.AppKey,
                MachineName = Environment.MachineName,
                Password = password
            }
        );
        await systemUserCredentials.Update
        (
            new CredentialValue
            (
                systemUser.UserName.Value,
                password
            )
        );
        var request = new RegisterAppRequest
        {
            AppTemplate = ToClientTemplateModel(template.ToModel()),
            VersionKey = versionKey
        };
        app = await hubClient.Install.RegisterApp(request);
        modKey = app.PublicKey;
    }

    private XTI_HubAppClient.AppApiTemplateModel ToClientTemplateModel(XTI_App.Api.AppApiTemplateModel model)
    {
        return new XTI_HubAppClient.AppApiTemplateModel
        {
            AppKey = model.AppKey,
            GroupTemplates = model.GroupTemplates
                .Select
                (
                    gt => new XTI_HubAppClient.AppApiGroupTemplateModel
                    {
                        Name = gt.Name,
                        IsAnonymousAllowed = gt.IsAnonymousAllowed,
                        ModCategory = gt.ModCategory,
                        Roles = gt.Roles,
                        ActionTemplates = gt.ActionTemplates
                            .Select
                            (
                                at => new XTI_HubAppClient.AppApiActionTemplateModel
                                {
                                    Name = at.Name,
                                    IsAnonymousAllowed = at.IsAnonymousAllowed,
                                    Roles = at.Roles,
                                    ResultType = at.ResultType
                                }
                            )
                            .ToArray()
                    }
                )
                .ToArray()
        };
    }
}