// Generated Code
namespace AuthenticatorWebApp.ApiControllers;
[AllowAnonymous]
public sealed partial class HomeController : Controller
{
    private readonly AuthenticatorAppApi api;
    public HomeController(AuthenticatorAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("Home").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data.ViewName);
    }
}