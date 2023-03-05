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

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("Apps").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppModel[]>> GetApps(CancellationToken ct)
    {
        return api.Group("Apps").Action<EmptyRequest, AppModel[]>("GetApps").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppDomainModel[]>> GetAppDomains(CancellationToken ct)
    {
        return api.Group("Apps").Action<EmptyRequest, AppDomainModel[]>("GetAppDomains").Execute(new EmptyRequest(), ct);
    }
}