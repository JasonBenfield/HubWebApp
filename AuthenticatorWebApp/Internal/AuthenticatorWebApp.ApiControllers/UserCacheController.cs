// Generated Code
namespace AuthenticatorWebApp.ApiControllers;
[Authorize]
public sealed partial class UserCacheController : Controller
{
    private readonly AuthenticatorAppApi api;
    public UserCacheController(AuthenticatorAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> ClearCache([FromBody] string requestData, CancellationToken ct)
    {
        return api.UserCache.ClearCache.Execute(requestData, ct);
    }
}