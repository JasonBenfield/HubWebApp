// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class AppController : Controller
{
    private readonly HubAppApi api;
    public AppController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index()
    {
        var result = await api.Group("App").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
        return View(result.Data.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<AppModel>> GetApp()
    {
        return api.Group("App").Action<EmptyRequest, AppModel>("GetApp").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel[]>> GetRoles()
    {
        return api.Group("App").Action<EmptyRequest, AppRoleModel[]>("GetRoles").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<AppRoleModel>> GetRole([FromBody] string model)
    {
        return api.Group("App").Action<string, AppRoleModel>("GetRole").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups()
    {
        return api.Group("App").Action<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] int model)
    {
        return api.Group("App").Action<int, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<AppEventModel[]>> GetMostRecentErrorEvents([FromBody] int model)
    {
        return api.Group("App").Action<int, AppEventModel[]>("GetMostRecentErrorEvents").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel[]>> GetModifierCategories()
    {
        return api.Group("App").Action<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories").Execute(new EmptyRequest());
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel>> GetModifierCategory([FromBody] string model)
    {
        return api.Group("App").Action<string, ModifierCategoryModel>("GetModifierCategory").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> GetDefaultModifier()
    {
        return api.Group("App").Action<EmptyRequest, ModifierModel>("GetDefaultModifier").Execute(new EmptyRequest());
    }
}