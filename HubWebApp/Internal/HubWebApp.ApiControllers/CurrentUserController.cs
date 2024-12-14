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
    public Task<ResultContainer<EmptyActionResult>> ChangePassword([FromBody] ChangeCurrentUserPasswordForm requestData, CancellationToken ct)
    {
        return api.CurrentUser.ChangePassword.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> EditUser([FromBody] EditCurrentUserForm requestData, CancellationToken ct)
    {
        return api.CurrentUser.EditUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUser(CancellationToken ct)
    {
        return api.CurrentUser.GetUser.Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var result = await api.CurrentUser.Index.Execute(new EmptyRequest(), ct);
        return View(result.Data!.ViewName);
    }
}