// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class UserMaintenanceController : Controller
{
    private readonly HubAppApi api;
    public UserMaintenanceController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> DeactivateUser([FromBody] int model, CancellationToken ct)
    {
        return api.Group("UserMaintenance").Action<int, AppUserModel>("DeactivateUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> ReactivateUser([FromBody] int model, CancellationToken ct)
    {
        return api.Group("UserMaintenance").Action<int, AppUserModel>("ReactivateUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> EditUser([FromBody] EditUserForm model, CancellationToken ct)
    {
        return api.Group("UserMaintenance").Action<EditUserForm, EmptyActionResult>("EditUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> ChangePassword([FromBody] ChangePasswordForm model, CancellationToken ct)
    {
        return api.Group("UserMaintenance").Action<ChangePasswordForm, EmptyActionResult>("ChangePassword").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<IDictionary<string, object>>> GetUserForEdit([FromBody] int model, CancellationToken ct)
    {
        return api.Group("UserMaintenance").Action<int, IDictionary<string, object>>("GetUserForEdit").Execute(model, ct);
    }
}