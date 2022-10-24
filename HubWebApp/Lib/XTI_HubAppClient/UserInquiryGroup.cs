// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserInquiryGroup : AppClientGroup
{
    public UserInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserInquiry")
    {
        Actions = new UserInquiryGroupActions(GetUser: CreatePostAction<int, AppUserModel>("GetUser"), GetUserOrAnon: CreatePostAction<string, AppUserModel>("GetUserOrAnon"));
    }

    public UserInquiryGroupActions Actions { get; }

    public Task<AppUserModel> GetUser(string modifier, int model, CancellationToken ct = default) => Actions.GetUser.Post(modifier, model, ct);
    public Task<AppUserModel> GetUserOrAnon(string modifier, string model, CancellationToken ct = default) => Actions.GetUserOrAnon.Post(modifier, model, ct);
    public sealed record UserInquiryGroupActions(AppClientPostAction<int, AppUserModel> GetUser, AppClientPostAction<string, AppUserModel> GetUserOrAnon);
}