using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public interface IHubAdministration
{
    Task<AppVersionModel> Version(AppKey appKey, AppVersionKey versionKey);

    Task<AppVersionModel> BeginPublish(AppKey appKey, AppVersionKey versionKey);

    Task<AppVersionModel> EndPublish(AppKey appKey, AppVersionKey versionKey);

    Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password);

    Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName);

    Task<int> BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName);

    Task<int> BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName);

    Task Installed(int installationID);

    Task<AppVersionKey> NextVersionKey();

    Task<AppVersionModel> StartNewVersion(AppKey appKey, string domain, AppVersionKey versionKey, AppVersionType versionType);
}