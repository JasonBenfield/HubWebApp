// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class UserCacheController : Controller
{
    private readonly HubAppApi api;
    public UserCacheController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> ClearCache([FromBody] string requestData, CancellationToken ct)
    {
        return api.UserCache.ClearCache.Execute(requestData, ct);
    }
}