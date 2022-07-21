// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserInquiryGroup : AppClientGroup
{
    public UserInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "UserInquiry")
    {
        Actions = new UserInquiryGroupActions(GetUser: CreatePostAction<int, AppUserModel>("GetUser"));
    }

    public UserInquiryGroupActions Actions { get; }

    public Task<AppUserModel> GetUser(string modifier, int model) => Actions.GetUser.Post(modifier, model);
    public sealed record UserInquiryGroupActions(AppClientPostAction<int, AppUserModel> GetUser);
}