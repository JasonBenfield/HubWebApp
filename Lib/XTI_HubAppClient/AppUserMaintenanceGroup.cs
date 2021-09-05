// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class AppUserMaintenanceGroup : AppClientGroup
    {
        public AppUserMaintenanceGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "AppUserMaintenance")
        {
        }

        public Task<int> AssignRole(string modifier, UserRoleRequest model) => Post<int, UserRoleRequest>("AssignRole", modifier, model);
        public Task<EmptyActionResult> UnassignRole(string modifier, UserRoleRequest model) => Post<EmptyActionResult, UserRoleRequest>("UnassignRole", modifier, model);
    }
}