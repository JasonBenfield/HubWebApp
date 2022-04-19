using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.Secrets;
using XTI_Credentials;
using XTI_HubAppClient;

namespace XTI_AppSetupApp.Extensions;

public sealed class DefaultAppSetup : IAppSetup
{
    private readonly HubAppClient hubClient;
    private readonly SystemUserCredentials systemUserCredentials;
    private readonly AppApiFactory apiFactory;
    private readonly VersionReader versionReader;
    private readonly string domain;
    private AppModel? app;
    private string? modKey;

    public DefaultAppSetup(HubAppClient hubClient, AppApiFactory apiFactory, SystemUserCredentials systemUserCredentials, VersionReader versionReader, SetupOptions options)
    {
        this.hubClient = hubClient;
        this.apiFactory = apiFactory;
        this.systemUserCredentials = systemUserCredentials;
        this.versionReader = versionReader;
        domain = options.Domain;
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
        var versions = await versionReader.Versions();
        var template = apiFactory.CreateTemplate();
        var password = Guid.NewGuid().ToString();
        var systemUser = await hubClient.Install.AddSystemUser
        (
            "",
            new AddSystemUserRequest
            {
                AppKey = template.AppKey,
                MachineName = Environment.MachineName,
                Domain = domain,
                Password = password
            }
        );
        await systemUserCredentials.Update
        (
            new CredentialValue
            (
                systemUser.UserName,
                password
            )
        );
        var request = new RegisterAppRequest
        {
            AppTemplate = ToClientTemplateModel(template.ToModel()),
            VersionKey = versionKey,
            Versions = versions
        };
        var result = await hubClient.Install.RegisterApp("", request);
        app = result.App;
        modKey = result.ModKey;
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