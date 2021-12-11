// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_HubAppApi.AppUserMaintenance;
using XTI_App.Api;
using XTI_HubAppApi;
using XTI_HubAppApi.UserMaintenance;
using XTI_App;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class AppUserMaintenanceController : Controller
    {
        public AppUserMaintenanceController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<int>> AssignRole([FromBody] UserRoleRequest model)
        {
            return api.Group("AppUserMaintenance").Action<UserRoleRequest, int>("AssignRole").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> UnassignRole([FromBody] UserRoleRequest model)
        {
            return api.Group("AppUserMaintenance").Action<UserRoleRequest, EmptyActionResult>("UnassignRole").Execute(model);
        }
    }
}