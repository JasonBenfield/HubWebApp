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

    [HttpPost]
    public Task<ResultContainer<AppModel>> GetApp(CancellationToken ct)
    {
        return api.App.GetApp.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> GetDefaultAppOptions(CancellationToken ct)
    {
        return api.App.GetDefaultAppOptions.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> GetDefaultModifier(CancellationToken ct)
    {
        return api.App.GetDefaultModifier.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> GetDefaultOptions(CancellationToken ct)
    {
        return api.App.GetDefaultOptions.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel[]>> GetModifierCategories(CancellationToken ct)
    {
        return api.App.GetModifierCategories.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppLogEntryModel[]>> GetMostRecentErrorEvents([FromBody] int requestData, CancellationToken ct)
    {
        return api.App.GetMostRecentErrorEvents.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] int requestData, CancellationToken ct)
    {
        return api.App.GetMostRecentRequests.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups(CancellationToken ct)
    {
        return api.App.GetResourceGroups.Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoles(CancellationToken ct)
    {
        return api.App.GetRoles.Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.App.Index.Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }
}