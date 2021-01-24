// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HubWebApp.Client
{
    public sealed partial class UsersGroup : AppClientGroup
    {
        public UsersGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Users")
        {
        }

        public Task<AppUserModel[]> GetUsers() => Post<AppUserModel[], EmptyRequest>("GetUsers", "", new EmptyRequest());
    }
}