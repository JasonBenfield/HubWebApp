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
    public Task<ResultContainer<AuthenticatedLoginResult>> ExternalAuthKey([FromBody] ExternalAuthKeyModel requestData, CancellationToken ct)
    {
        return api.ExternalAuth.ExternalAuthKey.Execute(requestData, ct);
    }
}