// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HubWebApp.Client
{
    public sealed partial class AppUserGroup : AppClientGroup
    {
        public AppUserGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "AppUser")
        {
        }

        public Task<AppUserRoleModel[]> GetUserRoles(string modifier, int model) => Post<AppUserRoleModel[], int>("GetUserRoles", modifier, model);
        public Task<UserRoleAccessModel> GetUserRoleAccess(string modifier, int model) => Post<UserRoleAccessModel, int>("GetUserRoleAccess", modifier, model);
    }
}