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
    public Task<ResultContainer<AppUserModel>> AddAdminUser([FromBody] AddAdminUserRequest requestData, CancellationToken ct)
    {
        return api.Install.AddAdminUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddInstallationUser([FromBody] AddInstallationUserRequest requestData, CancellationToken ct)
    {
        return api.Install.AddInstallationUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppModel[]>> AddOrUpdateApps([FromBody] AddOrUpdateAppsRequest requestData, CancellationToken ct)
    {
        return api.Install.AddOrUpdateApps.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> AddOrUpdateVersions([FromBody] AddOrUpdateVersionsRequest requestData, CancellationToken ct)
    {
        return api.Install.AddOrUpdateVersions.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddSystemUser([FromBody] AddSystemUserRequest requestData, CancellationToken ct)
    {
        return api.Install.AddSystemUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> BeginInstallation([FromBody] GetInstallationRequest requestData, CancellationToken ct)
    {
        return api.Install.BeginInstallation.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallConfigurationModel>> ConfigureInstall([FromBody] ConfigureInstallRequest requestData, CancellationToken ct)
    {
        return api.Install.ConfigureInstall.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallConfigurationTemplateModel>> ConfigureInstallTemplate([FromBody] ConfigureInstallTemplateRequest requestData, CancellationToken ct)
    {
        return api.Install.ConfigureInstallTemplate.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DeleteInstallConfiguration([FromBody] DeleteInstallConfigurationRequest requestData, CancellationToken ct)
    {
        return api.Install.DeleteInstallConfiguration.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallConfigurationModel[]>> GetInstallConfigurations([FromBody] GetInstallConfigurationsRequest requestData, CancellationToken ct)
    {
        return api.Install.GetInstallConfigurations.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> GetVersion([FromBody] GetVersionRequest requestData, CancellationToken ct)
    {
        return api.Install.GetVersion.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel[]>> GetVersions([FromBody] GetVersionsRequest requestData, CancellationToken ct)
    {
        return api.Install.GetVersions.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Installed([FromBody] GetInstallationRequest requestData, CancellationToken ct)
    {
        return api.Install.Installed.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<NewInstallationResult>> NewInstallation([FromBody] NewInstallationRequest requestData, CancellationToken ct)
    {
        return api.Install.NewInstallation.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppModel>> RegisterApp([FromBody] RegisterAppRequest requestData, CancellationToken ct)
    {
        return api.Install.RegisterApp.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> SetUserAccess([FromBody] SetUserAccessRequest requestData, CancellationToken ct)
    {
        return api.Install.SetUserAccess.Execute(requestData, ct);
    }
}