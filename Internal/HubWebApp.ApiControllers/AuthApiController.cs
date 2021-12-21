// Generated Code
namespace HubWebApp.ApiControllers;
[AllowAnonymous]
public class AuthApiController : Controller
{
    private readonly HubAppApi api;
    public AuthApiController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<LoginResult>> Authenticate([FromBody] LoginCredentials model)
    {
        return api.Group("AuthApi").Action<LoginCredentials, LoginResult>("Authenticate").Execute(model);
    }
}