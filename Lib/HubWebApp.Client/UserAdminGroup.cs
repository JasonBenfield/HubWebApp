// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HubWebApp.Client
{
    public sealed partial class UserAdminGroup : AppClientGroup
    {
        public UserAdminGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "UserAdmin")
        {
        }

        public Task<int> AddUser(AddUserModel model) => Post<int, AddUserModel>("AddUser", "", model);
    }
}