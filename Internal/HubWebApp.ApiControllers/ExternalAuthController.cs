// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class ExternalAuthController : Controller
{
    private readonly HubAppApi api;
    public ExternalAuthController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Login(ExternalLoginRequest model)
    {
        var result = await api.Group("ExternalAuth").Action<ExternalLoginRequest, WebRedirectResult>("Login").Execute(model);
        return Redirect(result.Data.Url);
    }
}