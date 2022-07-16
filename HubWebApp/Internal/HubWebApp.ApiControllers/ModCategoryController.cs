// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class ModCategoryController : Controller
{
    private readonly HubAppApi api;
    public ModCategoryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] int model, CancellationToken ct)
    {
        return api.Group("ModCategory").Action<int, ModifierCategoryModel>("GetModCategory").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel[]>> GetModifiers([FromBody] int model, CancellationToken ct)
    {
        return api.Group("ModCategory").Action<int, ModifierModel[]>("GetModifiers").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups([FromBody] int model, CancellationToken ct)
    {
        return api.Group("ModCategory").Action<int, ResourceGroupModel[]>("GetResourceGroups").Execute(model, ct);
    }
}