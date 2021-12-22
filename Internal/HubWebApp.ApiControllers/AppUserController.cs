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
    public Task<ResultContainer<AppRoleModel[]>> GetUserRoles([FromBody] GetUserRolesRequest model)
    {
        return api.Group("AppUser").Action<GetUserRolesRequest, AppRoleModel[]>("GetUserRoles").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<UserRoleAccessModel>> GetUserRoleAccess([FromBody] GetUserRoleAccessRequest model)
    {
        return api.Group("AppUser").Action<GetUserRoleAccessRequest, UserRoleAccessModel>("GetUserRoleAccess").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<UserModifierCategoryModel[]>> GetUserModCategories([FromBody] int model)
    {
        return api.Group("AppUser").Action<int, UserModifierCategoryModel[]>("GetUserModCategories").Execute(model);
    }
}