// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserInquiryGroup : AppClientGroup
{
    public UserInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserInquiry")
    {
        Actions = new UserInquiryGroupActions(GetUser: CreatePostAction<int, AppUserModel>("GetUser"), GetUserOrAnon: CreatePostAction<string, AppUserModel>("GetUserOrAnon"), GetUserAuthenticators: CreatePostAction<int, UserAuthenticatorModel[]>("GetUserAuthenticators"), GetUsers: CreatePostAction<EmptyRequest, AppUserModel[]>("GetUsers"));
    }

    public UserInquiryGroupActions Actions { get; }

    public Task<AppUserModel> GetUser(string modifier, int model, CancellationToken ct = default) => Actions.GetUser.Post(modifier, model, ct);
    public Task<AppUserModel> GetUserOrAnon(string modifier, string model, CancellationToken ct = default) => Actions.GetUserOrAnon.Post(modifier, model, ct);
    public Task<UserAuthenticatorModel[]> GetUserAuthenticators(string modifier, int model, CancellationToken ct = default) => Actions.GetUserAuthenticators.Post(modifier, model, ct);
    public Task<AppUserModel[]> GetUsers(string modifier, CancellationToken ct = default) => Actions.GetUsers.Post(modifier, new EmptyRequest(), ct);
    public sealed record UserInquiryGroupActions(AppClientPostAction<int, AppUserModel> GetUser, AppClientPostAction<string, AppUserModel> GetUserOrAnon, AppClientPostAction<int, UserAuthenticatorModel[]> GetUserAuthenticators, AppClientPostAction<EmptyRequest, AppUserModel[]> GetUsers);
}