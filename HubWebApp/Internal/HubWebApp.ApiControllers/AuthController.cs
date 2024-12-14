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

    public async Task<IActionResult> Login(AuthenticatedLoginRequest requestData, CancellationToken ct)
    {
        var result = await api.Auth.Login.Execute(requestData, ct);
        return Redirect(result.Data!.Url);
    }

    [HttpPost]
    public Task<ResultContainer<string>> LoginReturnKey([FromBody] LoginReturnModel requestData, CancellationToken ct)
    {
        return api.Auth.LoginReturnKey.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AuthenticatedLoginResult>> VerifyLogin([FromBody] VerifyLoginForm requestData, CancellationToken ct)
    {
        return api.Auth.VerifyLogin.Execute(requestData, ct);
    }

    [ResponseCache(CacheProfileName = "Default")]
    public async Task<IActionResult> VerifyLoginForm(CancellationToken ct)
    {
        var result = await api.Auth.VerifyLoginForm.Execute(new EmptyRequest(), ct);
        return PartialView(result.Data!.ViewName);
    }
}