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

    [HttpPost]
    public Task<ResultContainer<AppUserGroupModel>> AddUserGroupIfNotExists([FromBody] AddUserGroupIfNotExistsRequest requestData, CancellationToken ct)
    {
        return api.UserGroups.AddUserGroupIfNotExists.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserDetailModel>> GetUserDetailOrAnon([FromBody] AppUserNameRequest requestData, CancellationToken ct)
    {
        return api.UserGroups.GetUserDetailOrAnon.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserGroupModel>> GetUserGroupForUser([FromBody] AppUserIDRequest requestData, CancellationToken ct)
    {
        return api.UserGroups.GetUserGroupForUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserGroupModel[]>> GetUserGroups(CancellationToken ct)
    {
        return api.UserGroups.GetUserGroups.Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.UserGroups.Index.Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> UserQuery(UserGroupKey requestData, CancellationToken ct)
    {
        var result = await api.UserGroups.UserQuery.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}