using System.Text.RegularExpressions;
using XTI_App.Abstractions;
using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_Internal.Abstractions;
using XTI_WebAppClient;

namespace XTI_HubAppClient.Implementations;

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

    public Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, string repoOwner, string repoName, AppKey[] appKeys, CancellationToken ct) =>
        hubClient.Install.AddOrUpdateApps
        (
            new AddOrUpdateAppsRequest
            (
                versionName: versionName,
                repoOwner: repoOwner,
                repoName: repoName,
                appKeys: appKeys
            ),
            ct
        );

    public Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(ConfigureInstallTemplateRequest configRequest, CancellationToken ct) =>
        hubClient.InstallTemplates.ConfigureInstallTemplate(configRequest, ct);

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

    public Task<AppInstallCommandDetailModel> AddInstallCommand(AddInstallCommandRequest installRequest, CancellationToken ct) =>
        hubClient.Installations.AddInstallCommand(installRequest, ct);

    private static readonly Regex whitespaceRegex = new Regex("\\s+");

    public Task<AppCommandModel> BeginCommand(AppKey appKey, int requestedInstallationID, CancellationToken ct) =>
        hubClient.Command.BeginCommand(GetModifier(appKey), new(commandID: requestedInstallationID), ct);

    private static string GetModifier(AppKey appKey) =>
        whitespaceRegex.Replace(appKey.Format(), "");

    public Task<AppCommandStepModel> BeginCommandStep(AppKey appKey, int requestedInstallationID, string activity, CancellationToken ct) =>
        hubClient.Command.BeginCommandStep
        (
            GetModifier(appKey),
            new(commandID: requestedInstallationID, activity: activity),
            ct
        );

    public Task CommandStepEnded(AppKey appKey, int stepID, string errorMessage, CancellationToken ct) =>
        hubClient.Command.CommandStepEnded
        (
            GetModifier(appKey),
            new(stepID: stepID, errorMessage: errorMessage),
            ct
        );

    public Task<AppInstallCommandDetailModel> GetInstallCommandDetail(AppKey appKey, int requestedInstallationID, CancellationToken ct) =>
        hubClient.Command.GetInstallationCommandDetail
        (
            GetModifier(appKey),
            new(commandID: requestedInstallationID),
            ct
        );

    public Task<InstallationModel> BeginInstallation(AppKey appKey, int requestedInstallationID, bool isCurrent, CancellationToken ct) =>
        hubClient.Command.BeginInstallation
        (
            GetModifier(appKey),
            new(commandID: requestedInstallationID, isCurrent: isCurrent),
            ct
        );

    public Task Installed(AppKey appKey, int installationID, CancellationToken ct) =>
        hubClient.Installation.Installed(GetModifier(appKey), new InstallationIDRequest(installationID), ct);

    public Task CommandEnded(AppKey appKey, int requestedInstallationID, CancellationToken ct) =>
        hubClient.Command.CommandEnded(GetModifier(appKey), new(commandID: requestedInstallationID), ct);

    public Task<AppCommandSummaryModel[]> GetPendingCommands(AppCommandName[] commandNames, string[] machineNames, CancellationToken ct) =>
        hubClient.Commands.GetPendingCommands
        (
            new(commandNames: commandNames, machineNames: machineNames),
            ct
        );

    public Task<AppDeleteCommandDetailModel> GetDeleteCommandDetail(AppKey appKey, int commandID, CancellationToken ct) =>
        hubClient.Command.GetDeleteCommandDetail(GetModifier(appKey), new(commandID: commandID), ct);

    public Task<AppDeleteCommandDetailModel> AddDeleteCommand(AppKey appKey, int installationID, CancellationToken ct) =>
        hubClient.Installation.AddDeleteCommand(GetModifier(appKey), new(installationID: installationID), ct);

    public Task BeginDelete(AppKey appKey, int installationID, CancellationToken ct) =>
        hubClient.Installation.BeginDelete(GetModifier(appKey), new(installationID: installationID), ct);

    public Task Deleted(AppKey appKey, int installationID, CancellationToken ct) =>
        hubClient.Installation.Deleted(GetModifier(appKey), new(installationID: installationID), ct);

}