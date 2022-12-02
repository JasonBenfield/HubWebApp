// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class InstallationsController : Controller
{
    private readonly HubAppApi api;
    public InstallationsController(HubAppApi api)
    {
        this.api = api;
    }

    public async Task<IActionResult> Index(InstallationQueryRequest model, CancellationToken ct)
    {
        var result = await api.Group("Installations").Action<InstallationQueryRequest, WebViewResult>("Index").Execute(model, ct);
        return View(result.Data.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationModel[]>> GetPendingDeletes([FromBody] GetPendingDeletesRequest model, CancellationToken ct)
    {
        return api.Group("Installations").Action<GetPendingDeletesRequest, InstallationModel[]>("GetPendingDeletes").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> RequestDelete([FromBody] GetInstallationRequest model, CancellationToken ct)
    {
        return api.Group("Installations").Action<GetInstallationRequest, EmptyActionResult>("RequestDelete").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> BeginDelete([FromBody] GetInstallationRequest model, CancellationToken ct)
    {
        return api.Group("Installations").Action<GetInstallationRequest, EmptyActionResult>("BeginDelete").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Deleted([FromBody] GetInstallationRequest model, CancellationToken ct)
    {
        return api.Group("Installations").Action<GetInstallationRequest, EmptyActionResult>("Deleted").Execute(model, ct);
    }
}