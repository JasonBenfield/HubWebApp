using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public interface IHubAdministration
{
    Task<AppModel[]> AddOrUpdateApps(AppVersionName versionName, AppKey[] appKeys);

    Task<XtiVersionModel> StartNewVersion(AppVersionName versionName, AppVersionType versionType);

    Task<XtiVersionModel> Version(AppVersionName versionName, AppVersionKey versionKey);

    Task<XtiVersionModel[]> Versions(AppVersionName versionName);

    Task AddOrUpdateVersions(AppKey[] appKeys, AddVersionRequest[] publishedVersions);

    Task<XtiVersionModel> BeginPublish(AppVersionName versionName, AppVersionKey versionKey);

    Task<XtiVersionModel> EndPublish(AppVersionName versionName, AppVersionKey versionKey);

    Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password);

    Task<AppUserModel> AddOrUpdateSystemUser(AppKey appKey, string machineName, string password);

    Task<AppUserModel> AddOrUpdateAdminUser(AppKey appKey, AppUserName userName, string password);

    Task<NewInstallationResult> NewInstallation(AppVersionName versionName, AppKey appKey, string machineName, string domain, string siteName);

    Task BeginInstall(int installationID);

    Task Installed(int installationID);
}