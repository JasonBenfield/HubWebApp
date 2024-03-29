// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
public sealed partial class UserInquiryController : Controller
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
    public Task<ResultContainer<AppUserModel>> GetUserOrAnon([FromBody] string model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<string, AppUserModel>("GetUserOrAnon").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserAuthenticatorModel[]>> GetUserAuthenticators([FromBody] int model, CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<int, UserAuthenticatorModel[]>("GetUserAuthenticators").Execute(model, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsers(CancellationToken ct)
    {
        return api.Group("UserInquiry").Action<EmptyRequest, AppUserModel[]>("GetUsers").Execute(new EmptyRequest(), ct);
    }
}