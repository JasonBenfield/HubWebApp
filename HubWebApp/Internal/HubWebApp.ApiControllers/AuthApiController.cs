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
    public Task<ResultContainer<LoginResult>> Authenticate([FromBody] LoginCredentials model, CancellationToken ct)
    {
        return api.Group("AuthApi").Action<LoginCredentials, LoginResult>("Authenticate").Execute(model, ct);
    }
}