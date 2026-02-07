using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public interface IHubService
{
    Task<string> StoreSingleUse(StorageName storageName, GenerateKeyModel generateKey, object data, TimeSpan expireAfter, CancellationToken ct);

    Task<string> StoredObject(StorageName storageName, string storageKey, CancellationToken ct);

    Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppKey[] appKeys, CancellationToken ct);

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

    Task<AppInstallCommandDetailModel> AddInstallCommand(AddAppInstallCommandRequest installRequest, CancellationToken ct);

    Task<AppCommandModel> BeginInstallCommand(int commandID, CancellationToken ct);

    Task<AppCommandStepModel> BeginCommandStep(int requestedInstallationID, string activity, CancellationToken ct);

    Task CommandStepEnded(int stepID, string errorMessage, CancellationToken ct);

    Task<AppInstallCommandDetailModel> GetInstallCommandDetail(int requestedInstallationID, CancellationToken ct);

    Task<InstallationModel> BeginInstallation(int requestedInstallationID, bool isCurrent, CancellationToken ct);

    Task Installed(int installationID, CancellationToken ct);

    Task CommandEnded(int requestedInstallationID, CancellationToken ct);

}