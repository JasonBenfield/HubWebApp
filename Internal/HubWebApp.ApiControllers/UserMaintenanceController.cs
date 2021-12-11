// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App.Api;
using XTI_HubAppApi;
using XTI_HubAppApi.UserMaintenance;
using XTI_App;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class UserMaintenanceController : Controller
    {
        public UserMaintenanceController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> EditUser([FromBody] EditUserForm model)
        {
            return api.Group("UserMaintenance").Action<EditUserForm, EmptyActionResult>("EditUser").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<IDictionary<string, object>>> GetUserForEdit([FromBody] int model)
        {
            return api.Group("UserMaintenance").Action<int, IDictionary<string, object>>("GetUserForEdit").Execute(model);
        }
    }
}