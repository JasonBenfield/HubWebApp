// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace HubWebApp.client
{
    public sealed class UserAdminGroup : AppClientGroup
    {
        public UserAdminGroup(IHttpClientFactory httpClientFactory, XtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "UserAdmin")
        {
        }

        public Task<int> AddUser(AddUserModel model) => Post<int, AddUserModel>("AddUser", model);
    }
}