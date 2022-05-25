// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallGroup : AppClientGroup
{
    public InstallGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Install")
    {
    }

    public Task<AppWithModKeyModel> RegisterApp(RegisterAppRequest model) => Post<AppWithModKeyModel, RegisterAppRequest>("RegisterApp", "", model);
    public Task<AppModel[]> AddOrUpdateApps(AddOrUpdateAppsRequest model) => Post<AppModel[], AddOrUpdateAppsRequest>("AddOrUpdateApps", "", model);
    public Task<EmptyActionResult> AddOrUpdateVersions(AddOrUpdateVersionsRequest model) => Post<EmptyActionResult, AddOrUpdateVersionsRequest>("AddOrUpdateVersions", "", model);
    public Task<XtiVersionModel> GetVersion(GetVersionRequest model) => Post<XtiVersionModel, GetVersionRequest>("GetVersion", "", model);
    public Task<XtiVersionModel[]> GetVersions(GetVersionsRequest model) => Post<XtiVersionModel[], GetVersionsRequest>("GetVersions", "", model);
    public Task<AppUserModel> AddSystemUser(AddSystemUserRequest model) => Post<AppUserModel, AddSystemUserRequest>("AddSystemUser", "", model);
    public Task<AppUserModel> AddInstallationUser(AddInstallationUserRequest model) => Post<AppUserModel, AddInstallationUserRequest>("AddInstallationUser", "", model);
    public Task<NewInstallationResult> NewInstallation(NewInstallationRequest model) => Post<NewInstallationResult, NewInstallationRequest>("NewInstallation", "", model);
    public Task<EmptyActionResult> BeginInstallation(InstallationRequest model) => Post<EmptyActionResult, InstallationRequest>("BeginInstallation", "", model);
    public Task<EmptyActionResult> Installed(InstallationRequest model) => Post<EmptyActionResult, InstallationRequest>("Installed", "", model);
    public Task<EmptyActionResult> SetUserAccess(SetUserAccessRequest model) => Post<EmptyActionResult, SetUserAccessRequest>("SetUserAccess", "", model);
}