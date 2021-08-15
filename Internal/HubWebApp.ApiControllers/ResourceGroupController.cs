// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App;
using HubWebAppApi.Apps;
using HubWebAppApi;
using HubWebAppApi.Users;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class ResourceGroupController : Controller
    {
        public ResourceGroupController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<ResourceGroupModel>> GetResourceGroup([FromBody] int model)
        {
            return api.Group("ResourceGroup").Action<int, ResourceGroupModel>("GetResourceGroup").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ResourceModel[]>> GetResources([FromBody] int model)
        {
            return api.Group("ResourceGroup").Action<int, ResourceModel[]>("GetResources").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppRoleModel[]>> GetRoleAccess([FromBody] int model)
        {
            return api.Group("ResourceGroup").Action<int, AppRoleModel[]>("GetRoleAccess").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] int model)
        {
            return api.Group("ResourceGroup").Action<int, ModifierCategoryModel>("GetModCategory").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] GetResourceGroupLogRequest model)
        {
            return api.Group("ResourceGroup").Action<GetResourceGroupLogRequest, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppEventModel[]>> GetMostRecentErrorEvents([FromBody] GetResourceGroupLogRequest model)
        {
            return api.Group("ResourceGroup").Action<GetResourceGroupLogRequest, AppEventModel[]>("GetMostRecentErrorEvents").Execute(model);
        }
    }
}