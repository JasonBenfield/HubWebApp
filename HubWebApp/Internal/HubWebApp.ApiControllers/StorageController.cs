// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class StorageController : Controller
{
    private readonly HubAppApi api;
    public StorageController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    [AllowAnonymous]
    public Task<ResultContainer<string>> GetStoredObject([FromBody] GetStoredObjectRequest requestData, CancellationToken ct)
    {
        return api.Storage.GetStoredObject.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<string>> StoreObject([FromBody] StoreObjectRequest requestData, CancellationToken ct)
    {
        return api.Storage.StoreObject.Execute(requestData, ct);
    }
}