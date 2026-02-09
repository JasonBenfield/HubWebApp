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

    [HttpPost]
    public Task<ResultContainer<AppDeleteCommandDetailModel>> AddDeleteCommand([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.AddDeleteCommand.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppInstallCommandDetailModel>> AddInstallCommand([FromBody] AddInstallCommandRequest requestData, CancellationToken ct)
    {
        return api.Installations.AddInstallCommand.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppCommandModel>> BeginCommand([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginCommand.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppCommandStepModel>> BeginCommandStep([FromBody] BeginAppCommandStepRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginCommandStep.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> BeginDelete([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginDelete.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationModel>> BeginInstallation([FromBody] BeginInstallationRequest requestData, CancellationToken ct)
    {
        return api.Installations.BeginInstallation.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> CommandEnded([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.CommandEnded.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyRequest>> CommandStepEnded([FromBody] AppCommandStepEndedRequest requestData, CancellationToken ct)
    {
        return api.Installations.CommandStepEnded.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Deleted([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.Deleted.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppDeleteCommandDetailModel>> GetDeleteCommandDetail([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetDeleteCommandDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppInstallCommandDetailModel>> GetInstallationCommandDetail([FromBody] AppCommandIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetInstallationCommandDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<InstallationDetailModel>> GetInstallationDetail([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetInstallationDetail.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppCommandModel[]>> GetPendingCommands([FromBody] GetPendingCommandsRequest requestData, CancellationToken ct)
    {
        return api.Installations.GetPendingCommands.Execute(requestData, ct);
    }

    public async Task<IActionResult> Index(InstallationQueryRequest requestData, CancellationToken ct)
    {
        var result = await api.Installations.Index.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    public async Task<IActionResult> Installation(InstallationViewRequest requestData, CancellationToken ct)
    {
        var result = await api.Installations.Installation.Execute(requestData, ct);
        return View(result.Data!.ViewName);
    }

    [HttpPost]
    public Task<ResultContainer<EmptyActionResult>> Installed([FromBody] InstallationIDRequest requestData, CancellationToken ct)
    {
        return api.Installations.Installed.Execute(requestData, ct);
    }
}