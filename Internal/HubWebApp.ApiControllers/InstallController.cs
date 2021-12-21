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
    public Task<ResultContainer<EmptyActionResult>> RegisterApp([FromBody] RegisterAppRequest model)
    {
        return api.Group("Install").Action<RegisterAppRequest, EmptyActionResult>("RegisterApp").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppVersionModel>> GetVersion([FromBody] GetVersionRequest model)
    {
        return api.Group("Install").Action<GetVersionRequest, AppVersionModel>("GetVersion").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddSystemUser([FromBody] AddSystemUserRequest model)
    {
        return api.Group("Install").Action<AddSystemUserRequest, AppUserModel>("AddSystemUser").Execute(model);
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