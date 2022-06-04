// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class AppsController : Controller
{
    private readonly HubAppApi api;
    public AppsController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("Apps").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppWithModKeyModel[]>> GetApps(CancellationToken ct)
    {
        return api.Group("Apps").Action<EmptyRequest, AppWithModKeyModel[]>("GetApps").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppWithModKeyModel>> GetAppById([FromBody] GetAppByIDRequest model, CancellationToken ct)
    {
        return api.Group("Apps").Action<GetAppByIDRequest, AppWithModKeyModel>("GetAppById").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppWithModKeyModel>> GetAppByAppKey([FromBody] GetAppByAppKeyRequest model, CancellationToken ct)
    {
        return api.Group("Apps").Action<GetAppByAppKeyRequest, AppWithModKeyModel>("GetAppByAppKey").Execute(model, ct);
    }

    public async Task<IActionResult> RedirectToApp(int model, CancellationToken ct)
    {
        var result = await api.Group("Apps").Action<int, WebRedirectResult>("RedirectToApp").Execute(model, ct);
        return Redirect(result.Data.Url);
    }

    [HttpPost]
    public Task<ResultContainer<AppDomainModel[]>> GetAppDomains(CancellationToken ct)
    {
        return api.Group("Apps").Action<EmptyRequest, AppDomainModel[]>("GetAppDomains").Execute(new EmptyRequest(), ct);
    }
}