// Generated Code
namespace HubWebApp.ApiControllers;
[AllowAnonymous]
public sealed partial class AuthController : Controller
{
    private readonly HubAppApi api;
    public AuthController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<string>> VerifyLogin([FromBody] VerifyLoginForm model, CancellationToken ct)
    {
        return api.Group("Auth").Action<VerifyLoginForm, string>("VerifyLogin").Execute(model, ct);
    }

    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> VerifyLoginForm(CancellationToken ct)
    {
        var result = await api.Group("Auth").Action<EmptyRequest, WebPartialViewResult>("VerifyLoginForm").Execute(new EmptyRequest(), ct);
        return PartialView(result.Data!.ViewName);
    }

    public async Task<IActionResult> Login(LoginModel model, CancellationToken ct)
    {
        var result = await api.Group("Auth").Action<LoginModel, WebRedirectResult>("Login").Execute(model, ct);
        return Redirect(result.Data!.Url);
    }

    [HttpPost]
    [Authorize]
    public Task<ResultContainer<string>> LoginReturnKey([FromBody] LoginReturnModel model, CancellationToken ct)
    {
        return api.Group("Auth").Action<LoginReturnModel, string>("LoginReturnKey").Execute(model, ct);
    }
}