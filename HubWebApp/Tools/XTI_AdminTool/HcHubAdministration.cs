using System.Runtime.CompilerServices;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_HubAppClient;
using XTI_HubAppClient.Extensions;
using XTI_WebAppClient;

namespace XTI_AdminTool;

public sealed class HcHubAdministration : IHubAdministration
{
    private readonly HubAppClient hubClient;

    public HcHubAdministration(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
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

    public Task BeginInstall(int installationID, CancellationToken ct) =>
        hubClient.Install.BeginInstallation(new GetInstallationRequest(installationID), ct);

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

    public Task Installed(int installationID, CancellationToken ct) =>
        hubClient.Install.Installed(new GetInstallationRequest(installationID), ct);

    public Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName, string domain, string siteName, CancellationToken ct)
    {
        var request = new NewInstallationRequest
        (
            versionName: versionName,
            appKey: appKey,
            qualifiedMachineName: machineName,
            domain: domain,
            siteName: siteName
        );
        return hubClient.Install.NewInstallation(request, ct);
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
            hubClient.UseToken<InstallationUserXtiToken>();
        }
        return serialized;
    }
}