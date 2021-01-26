// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App;
using HubWebApp.Apps;
using HubWebApp.Api;
using HubWebApp.UserApi;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        public ResourceController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<ResourceModel>> GetResource([FromBody] int model)
        {
            return api.Group("Resource").Action<int, ResourceModel>("GetResource").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<RoleAccessModel>> GetRoleAccess([FromBody] int model)
        {
            return api.Group("Resource").Action<int, RoleAccessModel>("GetRoleAccess").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] GetResourceLogRequest model)
        {
            return api.Group("Resource").Action<GetResourceLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppEventModel[]>> GetMostRecentErrorEvents([FromBody] GetResourceLogRequest model)
        {
            return api.Group("Resource").Action<GetResourceLogRequest, AppEventModel[]>("GetMostRecentErrorEvents").Execute(model);
        }
    }
}