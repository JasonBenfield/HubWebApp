// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class AppUserController : Controller
{
    private readonly HubAppApi api;
    public AppUserController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(int model)
    {
        var result = await api.Group("AppUser").Action<int, WebViewResult>("Index").Execute(model);
        return View(result.Data.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<UserAccessModel>> GetUserAccess([FromBody] UserModifierKey model)
    {
        return api.Group("AppUser").Action<UserModifierKey, UserAccessModel>("GetUserAccess").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetUnassignedRoles([FromBody] UserModifierKey model)
    {
        return api.Group("AppUser").Action<UserModifierKey, AppRoleModel[]>("GetUnassignedRoles").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<UserModifierCategoryModel[]>> GetUserModCategories([FromBody] int model)
    {
        return api.Group("AppUser").Action<int, UserModifierCategoryModel[]>("GetUserModCategories").Execute(model);
    }
}