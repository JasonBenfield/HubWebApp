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
    public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] int requestData, CancellationToken ct)
    {
        return api.ModCategory.GetModCategory.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ModifierModel[]>> GetModifiers([FromBody] int requestData, CancellationToken ct)
    {
        return api.ModCategory.GetModifiers.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups([FromBody] int requestData, CancellationToken ct)
    {
        return api.ModCategory.GetResourceGroups.Execute(requestData, ct);
    }
}