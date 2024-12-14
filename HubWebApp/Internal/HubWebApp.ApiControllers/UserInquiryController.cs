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
    public Task<ResultContainer<AppUserModel>> GetUser([FromBody] AppUserIDRequest requestData, CancellationToken ct)
    {
        return api.UserInquiry.GetUser.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<UserAuthenticatorModel[]>> GetUserAuthenticators([FromBody] AppUserIDRequest requestData, CancellationToken ct)
    {
        return api.UserInquiry.GetUserAuthenticators.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel>> GetUserOrAnon([FromBody] AppUserNameRequest requestData, CancellationToken ct)
    {
        return api.UserInquiry.GetUserOrAnon.Execute(requestData, ct);
    }

    [HttpPost]
    public Task<ResultContainer<AppUserModel[]>> GetUsers(CancellationToken ct)
    {
        return api.UserInquiry.GetUsers.Execute(new EmptyRequest(), ct);
    }
}