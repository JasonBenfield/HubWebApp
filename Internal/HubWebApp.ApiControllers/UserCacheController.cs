// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_WebApp.Api;
using XTI_App.Api;
using HubWebAppApi;
using HubWebAppApi.Users;
using XTI_App;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class UserCacheController : Controller
    {
        public UserCacheController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> ClearCache([FromBody] ClearUserCacheRequest model)
        {
            return api.Group("UserCache").Action<ClearUserCacheRequest, EmptyActionResult>("ClearCache").Execute(model);
        }
    }
}