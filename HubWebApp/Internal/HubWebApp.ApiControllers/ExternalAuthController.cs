// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class ExternalAuthController : Controller
{
    private readonly HubAppApi api;
    public ExternalAuthController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<string>> ExternalAuthKey([FromBody] ExternalAuthKeyModel model, CancellationToken ct)
    {
        return api.Group("ExternalAuth").Action<ExternalAuthKeyModel, string>("ExternalAuthKey").Execute(model, ct);
    }
}