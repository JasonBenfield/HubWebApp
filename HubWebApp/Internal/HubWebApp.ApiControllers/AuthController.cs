// Generated Code
namespace HubWebApp.ApiControllers;
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly HubAppApi api;
    public AuthController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> VerifyLogin([FromBody] VerifyLoginForm model)
    {
        return api.Group("Auth").Action<VerifyLoginForm, EmptyActionResult>("VerifyLogin").Execute(model);
    }

    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> VerifyLoginForm()
    {
        var result = await api.Group("Auth").Action<EmptyRequest, WebPartialViewResult>("VerifyLoginForm").Execute(new EmptyRequest());
        return PartialView(result.Data.ViewName);
    }

    public async Task<IActionResult> Login(LoginModel model)
    {
        var result = await api.Group("Auth").Action<LoginModel, WebRedirectResult>("Login").Execute(model);
        return Redirect(result.Data.Url);
    }
}