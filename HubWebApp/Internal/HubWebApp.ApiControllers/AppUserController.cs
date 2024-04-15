// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class AppUserController : Controller
{
    private readonly HubAppApi api;
    public AppUserController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(GetAppUserRequest model, CancellationToken ct)
    {
        var result = await api.Group("AppUser").Action<GetAppUserRequest, WebViewResult>("Index").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<UserAccessModel>> GetExplicitUserAccess([FromBody] UserModifierKey model, CancellationToken ct)
    {
        return api.Group("AppUser").Action<UserModifierKey, UserAccessModel>("GetExplicitUserAccess").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetAssignedRoles([FromBody] UserModifierKey model, CancellationToken ct)
    {
        return api.Group("AppUser").Action<UserModifierKey, AppRoleModel[]>("GetAssignedRoles").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetExplicitlyUnassignedRoles([FromBody] UserModifierKey model, CancellationToken ct)
    {
        return api.Group("AppUser").Action<UserModifierKey, AppRoleModel[]>("GetExplicitlyUnassignedRoles").Execute(model, ct);
    }
}