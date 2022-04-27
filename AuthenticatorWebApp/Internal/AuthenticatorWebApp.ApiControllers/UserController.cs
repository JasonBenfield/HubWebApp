// Generated Code
namespace AuthenticatorWebApp.ApiControllers;
[Authorize]
public class UserController : Controller
{
    private readonly AuthenticatorAppApi api;
    public UserController(AuthenticatorAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(UserStartRequest model)
    {
        var result = await api.Group("User").Action<UserStartRequest, WebViewResult>("Index").Execute(model);
        return View(result.Data.ViewName);
    }

    public async Task<IActionResult> AccessDenied()
    {
        var result = await api.Group("User").Action<EmptyRequest, WebViewResult>("AccessDenied").Execute(new EmptyRequest());
        return View(result.Data.ViewName);
    }

    public async Task<IActionResult> Error()
    {
        var result = await api.Group("User").Action<EmptyRequest, WebViewResult>("Error").Execute(new EmptyRequest());
        return View(result.Data.ViewName);
    }
}