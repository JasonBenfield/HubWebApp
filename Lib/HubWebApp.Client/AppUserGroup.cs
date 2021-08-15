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

        public Task<AppRoleModel[]> GetUserRoles(string modifier, int model) => Post<AppRoleModel[], int>("GetUserRoles", modifier, model);
        public Task<UserRoleAccessModel> GetUserRoleAccess(string modifier, GetUserRoleAccessRequest model) => Post<UserRoleAccessModel, GetUserRoleAccessRequest>("GetUserRoleAccess", modifier, model);
        public Task<UserModifierCategoryModel[]> GetUserModCategories(string modifier, int model) => Post<UserModifierCategoryModel[], int>("GetUserModCategories", modifier, model);
    }
}