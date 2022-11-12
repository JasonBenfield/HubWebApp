// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class CurrentUserController : Controller
{
    private readonly HubAppApi api;
    public CurrentUserController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("CurrentUser").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data.ViewName);
    }
}