// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using HubWebAppApi.Users;
using XTI_App.Api;
using HubWebAppApi;
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
        public Task<ResultContainer<int>> AssignRole([FromBody] AssignRoleRequest model)
        {
            return api.Group("AppUserMaintenance").Action<AssignRoleRequest, int>("AssignRole").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> UnassignRole([FromBody] int model)
        {
            return api.Group("AppUserMaintenance").Action<int, EmptyActionResult>("UnassignRole").Execute(model);
        }
    }
}