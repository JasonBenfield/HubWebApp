// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public class UserInquiryController : Controller
{
    private readonly HubAppApi api;
    public UserInquiryController(HubAppApi api)
    {
        this.api = api;
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUser([FromBody] int model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<int, AppUserModel>("GetUser").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserByUserName([FromBody] string model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<string, AppUserModel>("GetUserByUserName").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetCurrentUser(CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<EmptyRequest, AppUserModel>("GetCurrentUser").Execute(new EmptyRequest(), ct);
    }

    public async Task<IActionResult> RedirectToAppUser(RedirectToAppUserRequest model, CancellationToken ct)
    {
        var result = await api.Group("UserInquiry").Action<RedirectToAppUserRequest, WebRedirectResult>("RedirectToAppUser").Execute(model, ct);
        return Redirect(result.Data.Url);
    }
}