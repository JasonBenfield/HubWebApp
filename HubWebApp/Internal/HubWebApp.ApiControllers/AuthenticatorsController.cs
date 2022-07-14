// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class AuthenticatorsController : Controller
{
    private readonly HubAppApi api;
    public AuthenticatorsController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RegisterAuthenticator(CancellationToken ct)
    {
        return api.Group("Authenticators").Action<EmptyRequest, EmptyActionResult>("RegisterAuthenticator").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RegisterUserAuthenticator([FromBody] RegisterUserAuthenticatorRequest model, CancellationToken ct)
    {
        return api.Group("Authenticators").Action<RegisterUserAuthenticatorRequest, EmptyActionResult>("RegisterUserAuthenticator").Execute(model, ct);
    }
}