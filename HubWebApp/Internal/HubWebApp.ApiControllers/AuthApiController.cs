// Generated Code
namespace HubWebApp.ApiControllers;
[AllowAnonymous]
public sealed partial class AuthApiController : Controller
{
    private readonly HubAppApi api;
    public AuthApiController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<LoginResult>> Authenticate([FromBody] AuthenticateRequest model, CancellationToken ct)
    {
        return api.Group("AuthApi").Action<AuthenticateRequest, LoginResult>("Authenticate").Execute(model, ct);
    }
}