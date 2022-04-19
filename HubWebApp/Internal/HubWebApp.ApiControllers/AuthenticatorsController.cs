// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class AuthenticatorsController : Controller
{
    private readonly HubAppApi api;
    public AuthenticatorsController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RegisterAuthenticator()
    {
        return api.Group("Authenticators").Action<EmptyRequest, EmptyActionResult>("RegisterAuthenticator").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RegisterUserAuthenticator([FromBody] RegisterUserAuthenticatorRequest model)
    {
        return api.Group("Authenticators").Action<RegisterUserAuthenticatorRequest, EmptyActionResult>("RegisterUserAuthenticator").Execute(model);
    }
}