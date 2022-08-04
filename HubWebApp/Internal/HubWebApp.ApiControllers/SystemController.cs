// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class SystemController : Controller
{
    private readonly HubAppApi api;
    public SystemController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppContextModel>> GetAppContext(CancellationToken ct)
    {
        return api.Group("System").Action<EmptyRequest, AppContextModel>("GetAppContext").Execute(new EmptyRequest(), ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserContextModel>> GetUserContext([FromBody] GetUserContextRequest model, CancellationToken ct)
    {
        return api.Group("System").Action<GetUserContextRequest, UserContextModel>("GetUserContext").Execute(model, ct);
    }
}