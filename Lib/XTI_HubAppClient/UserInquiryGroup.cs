// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserInquiryGroup : AppClientGroup
{
    public UserInquiryGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, AppClientUrl clientUrl) : base(httpClientFactory, xtiToken, clientUrl, "UserInquiry")
    {
    }

    public Task<AppUserModel> GetUser(int model) => Post<AppUserModel, int>("GetUser", "", model);
    public Task<AppUserModel> GetUserByUserName(string model) => Post<AppUserModel, string>("GetUserByUserName", "", model);
    public Task<AppUserModel> GetCurrentUser() => Post<AppUserModel, EmptyRequest>("GetCurrentUser", "", new EmptyRequest());
}