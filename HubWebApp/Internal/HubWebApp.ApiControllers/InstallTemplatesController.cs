// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class InstallTemplatesController : Controller
{
    private readonly HubAppApi api;
    public InstallTemplatesController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<InstallConfigurationTemplateModel[]>> GetInstallTemplates(CancellationToken ct)
    {
        return api.InstallTemplates.GetInstallTemplates.Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.InstallTemplates.Index.Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }
}