// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class InstallationsController : Controller
{
    private readonly HubAppApi api;
    public InstallationsController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(InstallationQueryRequest model, CancellationToken ct)
    {
        var result = await api.Group("Installations").Action<InstallationQueryRequest, WebViewResult>("Index").Execute(model, ct);
        return View(result.Data.ViewName);
    }
}