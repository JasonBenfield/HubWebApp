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

    public async Task<IActionResult> Index(InstallationQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.Installations.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }
}