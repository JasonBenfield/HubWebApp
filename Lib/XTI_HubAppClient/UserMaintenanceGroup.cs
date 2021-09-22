// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class UserMaintenanceGroup : AppClientGroup
    {
        public UserMaintenanceGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "UserMaintenance")
        {
        }

        public Task<EmptyActionResult> EditUser(EditUserForm model) => Post<EmptyActionResult, EditUserForm>("EditUser", "", model);
        public Task<IDictionary<string, object>> GetUserForEdit(int model) => Post<IDictionary<string, object>, int>("GetUserForEdit", "", model);
    }
}