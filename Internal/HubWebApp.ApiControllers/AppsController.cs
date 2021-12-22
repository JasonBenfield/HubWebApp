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
    public Task<ResultContainer<AppModel[]>> All()
    {
        return api.Group("Apps").Action<EmptyRequest, AppModel[]>("All").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<string>> GetAppModifierKey([FromBody] AppKey model)
    {
        return api.Group("Apps").Action<AppKey, string>("GetAppModifierKey").Execute(model);
    }

    public async Task<IActionResult> RedirectToApp(int model)
    {
        var result = await api.Group("Apps").Action<int, WebRedirectResult>("RedirectToApp").Execute(model);
        return Redirect(result.Data.Url);
    }
}