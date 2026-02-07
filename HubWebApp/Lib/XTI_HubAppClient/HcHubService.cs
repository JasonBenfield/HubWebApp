using XTI_HubAppClient;

namespace XTI_AdminTool;

public sealed class HcHubService : IHubService
{
    private readonly HubAppClient hubClient;
    private Action resetDefaultToken;

    public HcHubService(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
        resetDefaultToken = () => hubClient.UseToken<AnonymousXtiToken>();
    }

    public void UseDefaultToken<T>()
        where T : IXtiToken
    {
        resetDefaultToken = () => hubClient.UseToken<T>();
    }

    public Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password, CancellationToken ct)
    {
        var request = new AddInstallationUserRequest
        (
            machineName: machineName,
            password: password
        );
        return hubClient.Install.AddInstallationUser(request, ct);
    }

    public Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password, CancellationToken ct)
    {
        var request = new AddSystemUserRequest
        (
            appKey: appKey,
            machineName: machineName,
            password: password
        );
        return hubClient.Install.AddSystemUser(request, ct);
    }

    public Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password, CancellationToken ct) =>
        hubClient.Install.AddAdminUser
        (
            new AddAdminUserRequest
            (
                appKey: appKey,
                userName: userName,
                password: password
            ),
            ct
        );

    public Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var request = new PublishVersionRequest
        (
            versionName: versionName,
            versionKey: versionKey
        );
        return hubClient.Publish.BeginPublish(request, ct);
    }

    public Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var request = new PublishVersionRequest
        (
            versionName: versionName,
            versionKey: versionKey
        );
        return hubClient.Publish.EndPublish(request, ct);
    }

    public Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, CancellationToken ct)
    {
        var request = new NewVersionRequest
        (
            versionName: versionName,
            versionType: versionType
        );
        return hubClient.Publish.NewVersion(request, ct);
    }

    public Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct)
    {
        var request = new GetVersionRequest
        (
            versionName: versionName,
            versionKey: versionKey
        );
        return hubClient.Install.GetVersion(request, ct);
    }

    public Task<XtiVersionModel[]> Versions(AppVersionName versionName, CancellationToken ct) =>
        hubClient.Install.GetVersions(new GetVersionsRequest(versionName), ct);

    public Task AddOrUpdateVersions(AppKey[] appKeys, AddVersionRequest[] publishedVersions, CancellationToken ct) =>
        hubClient.Install.AddOrUpdateVersions
        (
            new AddOrUpdateVersionsRequest
            (
                apps: appKeys,
                versions: publishedVersions
            ),
            ct
        );

    public Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppKey[] appKeys, CancellationToken ct) =>
        hubClient.Install.AddOrUpdateApps
        (
            new AddOrUpdateAppsRequest
            (
                versionName: versionName,
                appKeys: appKeys
            ),
            ct
        );

    public Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(ConfigureInstallTemplateRequest configRequest, CancellationToken ct) =>
        hubClient.Install.ConfigureInstallTemplate(configRequest, ct);

    public Task<InstallConfigurationModel[]> InstallConfigurations(GetInstallConfigurationsRequest getRequest, CancellationToken ct) =>
        hubClient.Install.GetInstallConfigurations(getRequest, ct);

    public Task<InstallConfigurationModel> InstallConfiguration(int configurationID, CancellationToken ct) =>
        hubClient.Install.GetInstallConfiguration(new(configurationID: configurationID), ct);

    public Task<InstallConfigurationModel> ConfigureInstall(ConfigureInstallRequest configRequest, CancellationToken ct) =>
        hubClient.Install.ConfigureInstall(configRequest, ct);

    public Task DeleteInstallConfiguration(DeleteInstallConfigurationRequest deleteRequest, CancellationToken ct) =>
        hubClient.Install.DeleteInstallConfiguration(deleteRequest, ct);

    public Task<string> StoreSingleUse(StorageName storageName, GenerateKeyModel generateKey, object data, TimeSpan expireAfter, CancellationToken ct) =>
        hubClient.Storage.StoreObject
        (
            new StoreObjectRequest(storageName, XtiSerializer.Serialize(data), expireAfter, generateKey)
            {
                IsSingleUse = true
            },
            ct
        );

    public async Task<string> StoredObject(StorageName storageName, string storageKey, CancellationToken ct)
    {
        string serialized;
        hubClient.UseToken<AnonymousXtiToken>();
        try
        {
            serialized = await hubClient.Storage.GetStoredObject(new GetStoredObjectRequest(storageName, storageKey), ct);
        }
        finally
        {
            resetDefaultToken();
        }
        return serialized;
    }

    public Task<AppInstallCommandDetailModel> AddInstallCommand(AddAppInstallCommandRequest installRequest, CancellationToken ct) =>
        hubClient.Installations.RequestInstallation(installRequest, ct);

    public Task<AppCommandModel> BeginInstallCommand(int requestedInstallationID, CancellationToken ct) =>
        hubClient.Installations.BeginRequestedInstallation(new(commandID: requestedInstallationID), ct);

    public Task<AppCommandStepModel> BeginCommandStep(int requestedInstallationID, string activity, CancellationToken ct) =>
        hubClient.Installations.BeginRequestedInstallationStep
        (
            new(commandID: requestedInstallationID, activity: activity),
            ct
        );

    public Task CommandStepEnded(int stepID, string errorMessage, CancellationToken ct) =>
        hubClient.Installations.RequestedInstallationStepEnded
        (
            new(stepID: stepID, errorMessage: errorMessage),
            ct
        );

    public Task<AppInstallCommandDetailModel> GetInstallCommandDetail(int requestedInstallationID, CancellationToken ct) =>
        hubClient.Installations.GetRequestedInstallationDetail
        (
            new(commandID: requestedInstallationID), 
            ct
        );

    public Task<InstallationModel> BeginInstallation(int requestedInstallationID, bool isCurrent, CancellationToken ct) =>
        hubClient.Installations.BeginInstallation
        (
            new(commandID: requestedInstallationID, isCurrent: isCurrent),
            ct
        );

    public Task Installed(int installationID, CancellationToken ct) =>
        hubClient.Installations.Installed(new InstallationIDRequest(installationID), ct);

    public Task CommandEnded(int requestedInstallationID, CancellationToken ct) =>
        hubClient.Installations.RequestedInstallationEnded(new(commandID: requestedInstallationID), ct);
}