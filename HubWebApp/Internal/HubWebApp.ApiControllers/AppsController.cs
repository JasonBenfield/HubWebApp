// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class AppsController : Controller
{
    private readonly HubAppApi api;
    public AppsController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppDomainModel[]>> GetAppDomains(CancellationToken ct)
    {
        return api.Apps.GetAppDomains.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppModel[]>> GetApps(CancellationToken ct)
    {
        return api.Apps.GetApps.Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Apps.Index.Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }
}