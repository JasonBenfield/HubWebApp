// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class UserInquiryGroup : AppClientGroup
    {
        public UserInquiryGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "UserInquiry")
        {
        }

        public Task<AppUserModel> GetUser(int model) => Post<AppUserModel, int>("GetUser", "", model);
        public Task<AppUserModel> GetUserByUserName(string model) => Post<AppUserModel, string>("GetUserByUserName", "", model);
        public Task<AppUserModel> GetCurrentUser() => Post<AppUserModel, EmptyRequest>("GetCurrentUser", "", new EmptyRequest());
    }
}