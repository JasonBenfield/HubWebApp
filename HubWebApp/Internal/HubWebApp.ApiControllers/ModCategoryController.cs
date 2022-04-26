// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class ModCategoryController : Controller
{
    private readonly HubAppApi api;
    public ModCategoryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] int model)
    {
        return api.Group("ModCategory").Action<int, ModifierCategoryModel>("GetModCategory").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel[]>> GetModifiers([FromBody] int model)
    {
        return api.Group("ModCategory").Action<int, ModifierModel[]>("GetModifiers").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel>> GetModifier([FromBody] GetModCategoryModifierRequest model)
    {
        return api.Group("ModCategory").Action<GetModCategoryModifierRequest, ModifierModel>("GetModifier").Execute(model);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups([FromBody] int model)
    {
        return api.Group("ModCategory").Action<int, ResourceGroupModel[]>("GetResourceGroups").Execute(model);
    }
}