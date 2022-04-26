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

    public async Task<IActionResult> Index()
    {
        var result = await api.Group("Apps").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
        return View(result.Data.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<string>> GetAppDomain([FromBody] GetAppDomainRequest model)
    {
        return api.Group("Apps").Action<GetAppDomainRequest, string>("GetAppDomain").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppWithModKeyModel[]>> GetApps()
    {
        return api.Group("Apps").Action<EmptyRequest, AppWithModKeyModel[]>("GetApps").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<AppWithModKeyModel>> GetAppById([FromBody] GetAppByIDRequest model)
    {
        return api.Group("Apps").Action<GetAppByIDRequest, AppWithModKeyModel>("GetAppById").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppWithModKeyModel>> GetAppByAppKey([FromBody] GetAppByAppKeyRequest model)
    {
        return api.Group("Apps").Action<GetAppByAppKeyRequest, AppWithModKeyModel>("GetAppByAppKey").Execute(model);
    }

    public async Task<IActionResult> RedirectToApp(int model)
    {
        var result = await api.Group("Apps").Action<int, WebRedirectResult>("RedirectToApp").Execute(model);
        return Redirect(result.Data.Url);
    }
}