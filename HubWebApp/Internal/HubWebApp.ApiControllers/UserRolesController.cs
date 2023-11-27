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

    public async Task<IActionResult> Index(UserRoleQueryRequest model, CancellationToken ct)
    {
        var result = await api.Group("UserRoles").Action<UserRoleQueryRequest, WebViewResult>("Index").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<UserRoleDetailModel>> GetUserRoleDetail([FromBody] int model, CancellationToken ct)
    {
        return api.Group("UserRoles").Action<int, UserRoleDetailModel>("GetUserRoleDetail").Execute(model, ct);
    }
}