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

    public async Task<IActionResult> Index(int model, CancellationToken ct)
    {
        var result = await api.Group("AppUser").Action<int, WebViewResult>("Index").Execute(model, ct);
        return View(result.Data.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<UserAccessModel>> GetUserAccess([FromBody] UserModifierKey model, CancellationToken ct)
    {
        return api.Group("AppUser").Action<UserModifierKey, UserAccessModel>("GetUserAccess").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetUnassignedRoles([FromBody] UserModifierKey model, CancellationToken ct)
    {
        return api.Group("AppUser").Action<UserModifierKey, AppRoleModel[]>("GetUnassignedRoles").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserModifierCategoryModel[]>> GetUserModCategories([FromBody] int model, CancellationToken ct)
    {
        return api.Group("AppUser").Action<int, UserModifierCategoryModel[]>("GetUserModCategories").Execute(model, ct);
    }
}