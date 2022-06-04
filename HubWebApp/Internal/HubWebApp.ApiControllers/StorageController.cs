// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class StorageController : Controller
{
    private readonly HubAppApi api;
    public StorageController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<string>> StoreObject([FromBody] StoreObjectRequest model, CancellationToken ct)
    {
        return api.Group("Storage").Action<StoreObjectRequest, string>("StoreObject").Execute(model, ct);
    }

    [HttpPost]
    [AllowAnonymous]
    public Task<ResultContainer<string>> GetStoredObject([FromBody] GetStoredObjectRequest model, CancellationToken ct)
    {
        return api.Group("Storage").Action<GetStoredObjectRequest, string>("GetStoredObject").Execute(model, ct);
    }
}