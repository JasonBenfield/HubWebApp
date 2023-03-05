// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class UserGroupsController : Controller
{
    private readonly HubAppApi api;
    public UserGroupsController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("UserGroups").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> UserQuery(UserGroupKey model, CancellationToken ct)
    {
        var result = await api.Group("UserGroups").Action<UserGroupKey, WebViewResult>("UserQuery").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserGroupModel>> AddUserGroupIfNotExists([FromBody] AddUserGroupIfNotExistsRequest model, CancellationToken ct)
    {
        return api.Group("UserGroups").Action<AddUserGroupIfNotExistsRequest, AppUserGroupModel>("AddUserGroupIfNotExists").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserGroupModel[]>> GetUserGroups(CancellationToken ct)
    {
        return api.Group("UserGroups").Action<EmptyRequest, AppUserGroupModel[]>("GetUserGroups").Execute(new EmptyRequest(), ct);
    }
}