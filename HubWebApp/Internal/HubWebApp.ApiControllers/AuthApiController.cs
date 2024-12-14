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
    public Task<ResultContainer<LoginResult>> Authenticate([FromBody] AuthenticateRequest requestData, CancellationToken ct)
    {
        return api.AuthApi.Authenticate.Execute(requestData, ct);
    }
}