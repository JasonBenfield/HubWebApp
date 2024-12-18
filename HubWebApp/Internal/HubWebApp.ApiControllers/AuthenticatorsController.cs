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
    public Task<ResultContainer<EmptyActionResult>> MoveAuthenticator([FromBody] MoveAuthenticatorRequest requestData, CancellationToken ct)
    {
        return api.Authenticators.MoveAuthenticator.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AuthenticatorModel>> RegisterAuthenticator([FromBody] RegisterAuthenticatorRequest requestData, CancellationToken ct)
    {
        return api.Authenticators.RegisterAuthenticator.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AuthenticatorModel>> RegisterUserAuthenticator([FromBody] RegisterUserAuthenticatorRequest requestData, CancellationToken ct)
    {
        return api.Authenticators.RegisterUserAuthenticator.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> UserOrAnonByAuthenticator([FromBody] UserOrAnonByAuthenticatorRequest requestData, CancellationToken ct)
    {
        return api.Authenticators.UserOrAnonByAuthenticator.Execute(requestData, ct);
    }
}