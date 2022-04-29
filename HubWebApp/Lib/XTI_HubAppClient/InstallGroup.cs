// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallGroup : AppClientGroup
{
    public InstallGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "Install")
    {
    }

    public Task<AppWithModKeyModel> RegisterApp(string modifier, RegisterAppRequest model) => Post<AppWithModKeyModel, RegisterAppRequest>("RegisterApp", modifier, model);
    public Task<AppModel[]> AddOrUpdateApps(string modifier, AddOrUpdateAppsRequest model) => Post<AppModel[], AddOrUpdateAppsRequest>("AddOrUpdateApps", modifier, model);
    public Task<EmptyActionResult> AddOrUpdateVersions(string modifier, AddOrUpdateVersionsRequest model) => Post<EmptyActionResult, AddOrUpdateVersionsRequest>("AddOrUpdateVersions", modifier, model);
    public Task<XtiVersionModel> GetVersion(string modifier, GetVersionRequest model) => Post<XtiVersionModel, GetVersionRequest>("GetVersion", modifier, model);
    public Task<XtiVersionModel[]> GetVersions(string modifier, GetVersionsRequest model) => Post<XtiVersionModel[], GetVersionsRequest>("GetVersions", modifier, model);
    public Task<AppUserModel> AddSystemUser(string modifier, AddSystemUserRequest model) => Post<AppUserModel, AddSystemUserRequest>("AddSystemUser", modifier, model);
    public Task<AppUserModel> AddInstallationUser(string modifier, AddInstallationUserRequest model) => Post<AppUserModel, AddInstallationUserRequest>("AddInstallationUser", modifier, model);
    public Task<NewInstallationResult> NewInstallation(string modifier, NewInstallationRequest model) => Post<NewInstallationResult, NewInstallationRequest>("NewInstallation", modifier, model);
    public Task<int> BeginCurrentInstallation(string modifier, BeginInstallationRequest model) => Post<int, BeginInstallationRequest>("BeginCurrentInstallation", modifier, model);
    public Task<int> BeginVersionInstallation(string modifier, BeginInstallationRequest model) => Post<int, BeginInstallationRequest>("BeginVersionInstallation", modifier, model);
    public Task<EmptyActionResult> Installed(string modifier, InstalledRequest model) => Post<EmptyActionResult, InstalledRequest>("Installed", modifier, model);
    public Task<EmptyActionResult> SetUserAccess(string modifier, SetUserAccessRequest model) => Post<EmptyActionResult, SetUserAccessRequest>("SetUserAccess", modifier, model);
}