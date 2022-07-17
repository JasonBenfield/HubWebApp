// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserInquiryGroup : AppClientGroup
{
    public UserInquiryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "UserInquiry")
    {
    }

    public Task<AppUserModel> GetUser(string modifier, int model) => Post<AppUserModel, int>("GetUser", modifier, model);
}