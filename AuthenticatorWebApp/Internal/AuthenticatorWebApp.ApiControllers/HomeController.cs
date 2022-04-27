// Generated Code
namespace AuthenticatorWebApp.ApiControllers;
[AllowAnonymous]
public class HomeController : Controller
{
    private readonly AuthenticatorAppApi api;
    public HomeController(AuthenticatorAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index()
    {
        var result = await api.Group("Home").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
        return View(result.Data.ViewName);
    }
}