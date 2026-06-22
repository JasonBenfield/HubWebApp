using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Internal.Abstractions;

public interface IHubService
{
    Task<string> StoreSingleUse(StorageName storageName, GenerateKeyModel generateKey, object data, TimeSpan expireAfter, CancellationToken ct);

    Task<string> StoredObject(StorageName storageName, string storageKey, CancellationToken ct);

    Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, string repoOwner, string repoName, AppKey[] appKeys, CancellationToken ct);

    Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType, CancellationToken ct);

    Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct);

    Task<XtiVersionModel[]> Versions(AppVersionName versionName, CancellationToken ct);

    Task AddOrUpdateVersions(AppKey[] appKeys, AddVersionRequest[] publishedVersions, CancellationToken ct);

    Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct);

    Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey, CancellationToken ct);

    Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password, CancellationToken ct);

    Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password, CancellationToken ct);

    Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password, CancellationToken ct);

    Task<InstallConfigurationTemplateModel> ConfigureInstallTemplate(ConfigureInstallTemplateRequest configRequest, CancellationToken ct);

    Task<InstallConfigurationModel[]> InstallConfigurations(GetInstallConfigurationsRequest getRequest, CancellationToken ct);

    Task<InstallConfigurationModel> InstallConfiguration(int configurationID, CancellationToken ct);

    Task<InstallConfigurationModel> ConfigureInstall(ConfigureInstallRequest configRequest, CancellationToken ct);

    Task DeleteInstallConfiguration(DeleteInstallConfigurationRequest deleteRequest, CancellationToken ct);

    Task<AppCommandSummaryModel[]> GetPendingCommands(AppCommandName[] commandNames, string[] machineNames, CancellationToken ct);

    Task<AppInstallCommandDetailModel> AddInstallCommand(AppKey appKey, AddInstallCommandRequest installRequest, CancellationToken ct);

    Task<AppCommandModel> BeginCommand(AppKey appKey, int commandID, CancellationToken ct);

    Task<AppCommandStepModel> BeginCommandStep(AppKey appKey, int commandID, string activity, CancellationToken ct);

    Task CommandStepEnded(AppKey appKey, int stepID, string errorMessage, CancellationToken ct);

    Task<AppInstallCommandDetailModel> GetInstallCommandDetail(AppKey appKey, int commandID, CancellationToken ct);

    Task<InstallationModel> BeginInstallation(AppKey appKey, int commandID, bool isCurrent, CancellationToken ct);

    Task Installed(AppKey appKey, int installationID, CancellationToken ct);

    Task<AppDeleteCommandDetailModel> GetDeleteCommandDetail(AppKey appKey, int commandID, CancellationToken ct);

    Task<AppDeleteCommandDetailModel> AddDeleteCommand(AppKey appKey, int installationID, CancellationToken ct);

    Task BeginDelete(AppKey appKey, int installationID, CancellationToken ct);

    Task Deleted(AppKey appKey, int installationID, CancellationToken ct);

    Task CommandEnded(AppKey appKey, int commandID, CancellationToken ct);

}