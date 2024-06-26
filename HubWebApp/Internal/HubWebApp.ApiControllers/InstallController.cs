// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class InstallController : Controller
{
    private readonly HubAppApi api;
    public InstallController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppModel>> RegisterApp([FromBody] RegisterAppRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<RegisterAppRequest, AppModel>("RegisterApp").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppModel[]>> AddOrUpdateApps([FromBody] AddOrUpdateAppsRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<AddOrUpdateAppsRequest, AppModel[]>("AddOrUpdateApps").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> AddOrUpdateVersions([FromBody] AddOrUpdateVersionsRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<AddOrUpdateVersionsRequest, EmptyActionResult>("AddOrUpdateVersions").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> GetVersion([FromBody] GetVersionRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<GetVersionRequest, XtiVersionModel>("GetVersion").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel[]>> GetVersions([FromBody] GetVersionsRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<GetVersionsRequest, XtiVersionModel[]>("GetVersions").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddSystemUser([FromBody] AddSystemUserRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<AddSystemUserRequest, AppUserModel>("AddSystemUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddAdminUser([FromBody] AddAdminUserRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<AddAdminUserRequest, AppUserModel>("AddAdminUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddInstallationUser([FromBody] AddInstallationUserRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<AddInstallationUserRequest, AppUserModel>("AddInstallationUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> BeginInstallation([FromBody] GetInstallationRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<GetInstallationRequest, EmptyActionResult>("BeginInstallation").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallConfigurationTemplateModel>> ConfigureInstallTemplate([FromBody] ConfigureInstallTemplateRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<ConfigureInstallTemplateRequest, InstallConfigurationTemplateModel>("ConfigureInstallTemplate").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallConfigurationModel>> ConfigureInstall([FromBody] ConfigureInstallRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<ConfigureInstallRequest, InstallConfigurationModel>("ConfigureInstall").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DeleteInstallConfiguration([FromBody] DeleteInstallConfigurationRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<DeleteInstallConfigurationRequest, EmptyActionResult>("DeleteInstallConfiguration").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallConfigurationModel[]>> GetInstallConfigurations([FromBody] GetInstallConfigurationsRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<GetInstallConfigurationsRequest, InstallConfigurationModel[]>("GetInstallConfigurations").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Installed([FromBody] GetInstallationRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<GetInstallationRequest, EmptyActionResult>("Installed").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<NewInstallationResult>> NewInstallation([FromBody] NewInstallationRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<NewInstallationRequest, NewInstallationResult>("NewInstallation").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> SetUserAccess([FromBody] SetUserAccessRequest model, CancellationToken ct)
    {
        return api.Group("Install").Action<SetUserAccessRequest, EmptyActionResult>("SetUserAccess").Execute(model, ct);
    }
}