// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class HomeController : Controller
{
    private readonly HubAppApi api;
    public HomeController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("Home").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }
}