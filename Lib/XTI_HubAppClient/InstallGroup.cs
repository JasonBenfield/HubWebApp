// Generated Code
namespace XTI_HubAppClient;
public sealed partial class InstallGroup : AppClientGroup
{
    public InstallGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl) : base(httpClientFactory, xtiToken, baseUrl, "Install")
    {
    }

    public Task<EmptyActionResult> RegisterApp(string modifier, RegisterAppRequest model) => Post<EmptyActionResult, RegisterAppRequest>("RegisterApp", modifier, model);
    public Task<AppVersionModel> GetVersion(string modifier, GetVersionRequest model) => Post<AppVersionModel, GetVersionRequest>("GetVersion", modifier, model);
    public Task<AppUserModel> AddSystemUser(string modifier, AddSystemUserRequest model) => Post<AppUserModel, AddSystemUserRequest>("AddSystemUser", modifier, model);
    public Task<NewInstallationResult> NewInstallation(string modifier, NewInstallationRequest model) => Post<NewInstallationResult, NewInstallationRequest>("NewInstallation", modifier, model);
    public Task<int> BeginCurrentInstallation(string modifier, BeginInstallationRequest model) => Post<int, BeginInstallationRequest>("BeginCurrentInstallation", modifier, model);
    public Task<int> BeginVersionInstallation(string modifier, BeginInstallationRequest model) => Post<int, BeginInstallationRequest>("BeginVersionInstallation", modifier, model);
    public Task<EmptyActionResult> Installed(string modifier, InstalledRequest model) => Post<EmptyActionResult, InstalledRequest>("Installed", modifier, model);
}