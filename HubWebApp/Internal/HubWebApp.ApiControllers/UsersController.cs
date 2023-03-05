// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class UsersController : Controller
{
    private readonly HubAppApi api;
    public UsersController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(GetUserRequest model, CancellationToken ct)
    {
        var result = await api.Group("Users").Action<GetUserRequest, WebViewResult>("Index").Execute(model, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserGroupModel>> GetUserGroup(CancellationToken ct)
    {
        return api.Group("Users").Action<EmptyRequest, AppUserGroupModel>("GetUserGroup").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsers(CancellationToken ct)
    {
        return api.Group("Users").Action<EmptyRequest, AppUserModel[]>("GetUsers").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddOrUpdateUser([FromBody] AddOrUpdateUserRequest model, CancellationToken ct)
    {
        return api.Group("Users").Action<AddOrUpdateUserRequest, AppUserModel>("AddOrUpdateUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddUser([FromBody] AddUserForm model, CancellationToken ct)
    {
        return api.Group("Users").Action<AddUserForm, AppUserModel>("AddUser").Execute(model, ct);
    }
}