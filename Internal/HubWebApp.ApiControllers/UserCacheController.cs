// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class UserCacheController : Controller
{
    private readonly HubAppApi api;
    public UserCacheController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> ClearCache([FromBody] string model)
    {
        return api.Group("UserCache").Action<string, EmptyActionResult>("ClearCache").Execute(model);
    }
}