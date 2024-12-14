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

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddOrUpdateUser([FromBody] AddOrUpdateUserRequest requestData, CancellationToken ct)
    {
        return api.Users.AddOrUpdateUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> AddUser([FromBody] AddUserForm requestData, CancellationToken ct)
    {
        return api.Users.AddUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserGroupModel>> GetUserGroup(CancellationToken ct)
    {
        return api.Users.GetUserGroup.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsers(CancellationToken ct)
    {
        return api.Users.GetUsers.Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(UsersIndexRequest requestData, CancellationToken ct)
    {
        var result = await api.Users.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}