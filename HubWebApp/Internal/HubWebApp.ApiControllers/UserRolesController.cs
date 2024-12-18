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
    public Task<ResultContainer<EmptyActionResult>> DeleteUserRole([FromBody] UserRoleIDRequest requestData, CancellationToken ct)
    {
        return api.UserRoles.DeleteUserRole.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserRoleDetailModel>> GetUserRoleDetail([FromBody] UserRoleIDRequest requestData, CancellationToken ct)
    {
        return api.UserRoles.GetUserRoleDetail.Execute(requestData, ct);
    }

    public async Task<IActionResult> Index(UserRoleQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.UserRoles.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> UserRole(UserRoleIDRequest requestData, CancellationToken ct)
    {
        var result = await api.UserRoles.UserRole.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}