// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class InstallController : Controller
{
    private readonly HubAppApi api;
    public InstallController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppWithModKeyModel>> RegisterApp([FromBody] RegisterAppRequest model)
    {
        return api.Group("Install").Action<RegisterAppRequest, AppWithModKeyModel>("RegisterApp").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> AddOrUpdateVersions([FromBody] AddOrUpdateVersionsRequest model)
    {
        return api.Group("Install").Action<AddOrUpdateVersionsRequest, EmptyActionResult>("AddOrUpdateVersions").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel>> GetVersion([FromBody] GetVersionRequest model)
    {
        return api.Group("Install").Action<GetVersionRequest, XtiVersionModel>("GetVersion").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<XtiVersionModel[]>> GetVersions([FromBody] GetVersionsRequest model)
    {
        return api.Group("Install").Action<GetVersionsRequest, XtiVersionModel[]>("GetVersions").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddSystemUser([FromBody] AddSystemUserRequest model)
    {
        return api.Group("Install").Action<AddSystemUserRequest, AppUserModel>("AddSystemUser").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddInstallationUser([FromBody] AddInstallationUserRequest model)
    {
        return api.Group("Install").Action<AddInstallationUserRequest, AppUserModel>("AddInstallationUser").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<NewInstallationResult>> NewInstallation([FromBody] NewInstallationRequest model)
    {
        return api.Group("Install").Action<NewInstallationRequest, NewInstallationResult>("NewInstallation").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<int>> BeginCurrentInstallation([FromBody] BeginInstallationRequest model)
    {
        return api.Group("Install").Action<BeginInstallationRequest, int>("BeginCurrentInstallation").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<int>> BeginVersionInstallation([FromBody] BeginInstallationRequest model)
    {
        return api.Group("Install").Action<BeginInstallationRequest, int>("BeginVersionInstallation").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Installed([FromBody] InstalledRequest model)
    {
        return api.Group("Install").Action<InstalledRequest, EmptyActionResult>("Installed").Execute(model);
    }
}