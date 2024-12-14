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
    public Task<ResultContainer<EmptyActionResult>> ChangePassword([FromBody] ChangePasswordForm requestData, CancellationToken ct)
    {
        return api.UserMaintenance.ChangePassword.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> DeactivateUser([FromBody] int requestData, CancellationToken ct)
    {
        return api.UserMaintenance.DeactivateUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> EditUser([FromBody] EditUserForm requestData, CancellationToken ct)
    {
        return api.UserMaintenance.EditUser.Execute(requestData, ct);
    }

    [HttpPost]
    public async Task<ResultContainer<IDictionary<string, object>>> GetUserForEdit([FromBody] int requestData, CancellationToken ct)
    {
        var result = await api.UserMaintenance.GetUserForEdit.Execute(requestData, ct);
        return result!;
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> ReactivateUser([FromBody] int requestData, CancellationToken ct)
    {
        return api.UserMaintenance.ReactivateUser.Execute(requestData, ct);
    }
}