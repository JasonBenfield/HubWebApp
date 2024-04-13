// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class AppController : Controller
{
    private readonly HubAppApi api;
    public AppController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("App").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppModel>> GetApp(CancellationToken ct)
    {
        return api.Group("App").Action<EmptyRequest, AppModel>("GetApp").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups(CancellationToken ct)
    {
        return api.Group("App").Action<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoles(CancellationToken ct)
    {
        return api.Group("App").Action<EmptyRequest, AppRoleModel[]>("GetRoles").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] int model, CancellationToken ct)
    {
        return api.Group("App").Action<int, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel[]>> GetMostRecentErrorEvents([FromBody] int model, CancellationToken ct)
    {
        return api.Group("App").Action<int, AppLogEntryModel[]>("GetMostRecentErrorEvents").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel[]>> GetModifierCategories(CancellationToken ct)
    {
        return api.Group("App").Action<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> GetDefaultModifier(CancellationToken ct)
    {
        return api.Group("App").Action<EmptyRequest, ModifierModel>("GetDefaultModifier").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> GetDefaultOptions(CancellationToken ct)
    {
        return api.Group("App").Action<EmptyRequest, string>("GetDefaultOptions").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> GetDefaultAppOptions(CancellationToken ct)
    {
        return api.Group("App").Action<EmptyRequest, string>("GetDefaultAppOptions").Execute(new EmptyRequest(), ct);
    }
}