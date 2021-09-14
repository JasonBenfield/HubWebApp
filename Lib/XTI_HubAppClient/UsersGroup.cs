// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class UsersGroup : AppClientGroup
    {
        public UsersGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Users")
        {
        }

        public Task<AppUserModel[]> GetUsers() => Post<AppUserModel[], EmptyRequest>("GetUsers", "", new EmptyRequest());
        public Task<AppUserModel[]> GetSystemUsers(AppKey model) => Post<AppUserModel[], AppKey>("GetSystemUsers", "", model);
        public Task<int> AddUser(AddUserModel model) => Post<int, AddUserModel>("AddUser", "", model);
    }
}