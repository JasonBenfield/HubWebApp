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