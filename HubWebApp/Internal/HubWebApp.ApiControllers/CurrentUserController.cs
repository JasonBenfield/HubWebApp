// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class CurrentUserController : Controller
{
    private readonly HubAppApi api;
    public CurrentUserController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> EditUser([FromBody] EditCurrentUserForm model, CancellationToken ct)
    {
        return api.Group("CurrentUser").Action<EditCurrentUserForm, AppUserModel>("EditUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUser(CancellationToken ct)
    {
        return api.Group("CurrentUser").Action<EmptyRequest, AppUserModel>("GetUser").Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.Group("CurrentUser").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest(), ct);
        return View(result.Data.ViewName);
    }
}