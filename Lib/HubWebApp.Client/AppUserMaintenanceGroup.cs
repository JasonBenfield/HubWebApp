// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HubWebApp.Client
{
    public sealed partial class AppUserMaintenanceGroup : AppClientGroup
    {
        public AppUserMaintenanceGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "AppUserMaintenance")
        {
        }

        public Task<int> AssignRole(string modifier, AssignRoleRequest model) => Post<int, AssignRoleRequest>("AssignRole", modifier, model);
        public Task<EmptyActionResult> UnassignRole(string modifier, int model) => Post<EmptyActionResult, int>("UnassignRole", modifier, model);
    }
}