// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class UserMaintenanceController : Controller
{
    private readonly HubAppApi api;
    public UserMaintenanceController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> EditUser([FromBody] EditUserForm model)
    {
        return api.Group("UserMaintenance").Action<EditUserForm, EmptyActionResult>("EditUser").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<IDictionary<string, object>>> GetUserForEdit([FromBody] int model)
    {
        return api.Group("UserMaintenance").Action<int, IDictionary<string, object>>("GetUserForEdit").Execute(model);
    }
}