// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserInquiryGroup : AppClientGroup
{
    public UserInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserInquiry")
    {
        Actions = new UserInquiryGroupActions(GetUser: CreatePostAction<AppUserIDRequest, AppUserModel>("GetUser"), GetUserAuthenticators: CreatePostAction<AppUserIDRequest, UserAuthenticatorModel[]>("GetUserAuthenticators"), GetUserOrAnon: CreatePostAction<AppUserNameRequest, AppUserModel>("GetUserOrAnon"), GetUsers: CreatePostAction<EmptyRequest, AppUserModel[]>("GetUsers"));
        Configure();
    }

    partial void Configure();
    public UserInquiryGroupActions Actions { get; }

    public Task<AppUserModel> GetUser(string modifier, AppUserIDRequest requestData, CancellationToken ct = default) => Actions.GetUser.Post(modifier, requestData, ct);
    public Task<UserAuthenticatorModel[]> GetUserAuthenticators(string modifier, AppUserIDRequest requestData, CancellationToken ct = default) => Actions.GetUserAuthenticators.Post(modifier, requestData, ct);
    public Task<AppUserModel> GetUserOrAnon(string modifier, AppUserNameRequest requestData, CancellationToken ct = default) => Actions.GetUserOrAnon.Post(modifier, requestData, ct);
    public Task<AppUserModel[]> GetUsers(string modifier, CancellationToken ct = default) => Actions.GetUsers.Post(modifier, new EmptyRequest(), ct);
    public sealed record UserInquiryGroupActions(AppClientPostAction<AppUserIDRequest, AppUserModel> GetUser, AppClientPostAction<AppUserIDRequest, UserAuthenticatorModel[]> GetUserAuthenticators, AppClientPostAction<AppUserNameRequest, AppUserModel> GetUserOrAnon, AppClientPostAction<EmptyRequest, AppUserModel[]> GetUsers);
}