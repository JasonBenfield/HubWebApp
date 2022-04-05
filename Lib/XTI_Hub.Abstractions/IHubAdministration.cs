using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public interface IHubAdministration
{
    Task<XtiVersionModel> StartNewVersion(string groupName, AppVersionType versionType, AppDefinitionModel[] appDefs);

    Task<XtiVersionModel> Version(string groupName, AppVersionKey versionKey);

    Task<XtiVersionModel> BeginPublish(string groupName, AppVersionKey versionKey);

    Task<XtiVersionModel> EndPublish(string groupName, AppVersionKey versionKey);

    Task<AppUserModel> AddOrUpdateInstallationUser(string machineName, string password);

    Task<NewInstallationResult> NewInstallation(AppKey appKey, string machineName);

    Task<int> BeginCurrentInstall(AppKey appKey, AppVersionKey installVersionKey, string machineName);

    Task<int> BeginVersionInstall(AppKey appKey, AppVersionKey versionKey, string machineName);

    Task Installed(int installationID);
}