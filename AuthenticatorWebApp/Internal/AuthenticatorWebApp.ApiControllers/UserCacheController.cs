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
    public Task<ResultContainer<EmptyActionResult>> ClearCache([FromBody] string model, CancellationToken ct)
    {
        return api.Group("UserCache").Action<string, EmptyActionResult>("ClearCache").Execute(model, ct);
    }
}