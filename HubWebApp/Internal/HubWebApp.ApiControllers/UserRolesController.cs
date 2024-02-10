// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class UserRolesController : Controller
{
    private readonly HubAppApi api;
    public UserRolesController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> DeleteUserRole([FromBody] UserRoleIDRequest model, CancellationToken ct)
    {
        return api.Group("UserRoles").Action<UserRoleIDRequest, EmptyActionResult>("DeleteUserRole").Execute(model, ct);
    }

    public async Task<IActionResult> Index(UserRoleQueryRequest model, CancellationToken ct)
    {
        var result = await api.Group("UserRoles").Action<UserRoleQueryRequest, WebViewResult>("Index").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<UserRoleDetailModel>> GetUserRoleDetail([FromBody] UserRoleIDRequest model, CancellationToken ct)
    {
        return api.Group("UserRoles").Action<UserRoleIDRequest, UserRoleDetailModel>("GetUserRoleDetail").Execute(model, ct);
    }

    public async Task<IActionResult> UserRole(UserRoleIDRequest model, CancellationToken ct)
    {
        var result = await api.Group("UserRoles").Action<UserRoleIDRequest, WebViewResult>("UserRole").Execute(model, ct);
        return View(result.Data!.ViewName);
    }
}