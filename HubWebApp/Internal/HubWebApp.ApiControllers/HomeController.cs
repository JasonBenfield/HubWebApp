// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class HomeController : Controller
{
    private readonly HubAppApi api;
    public HomeController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index()
    {
        var result = await api.Group("Home").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
        return View(result.Data.ViewName);
    }
}