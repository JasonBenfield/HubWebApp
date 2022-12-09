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
    public Task<ResultContainer<AuthenticatorModel>> RegisterAuthenticator([FromBody] RegisterAuthenticatorRequest model, CancellationToken ct)
    {
        return api.Group("Authenticators").Action<RegisterAuthenticatorRequest, AuthenticatorModel>("RegisterAuthenticator").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AuthenticatorModel>> RegisterUserAuthenticator([FromBody] RegisterUserAuthenticatorRequest model, CancellationToken ct)
    {
        return api.Group("Authenticators").Action<RegisterUserAuthenticatorRequest, AuthenticatorModel>("RegisterUserAuthenticator").Execute(model, ct);
    }
}