// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_HubAppApi.ResourceGroupInquiry;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.UserMaintenance;
using XTI_App;
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
        public Task<ResultContainer<ResourceGroupModel>> GetResourceGroup([FromBody] GetResourceGroupRequest model)
        {
            return api.Group("ResourceGroup").Action<GetResourceGroupRequest, ResourceGroupModel>("GetResourceGroup").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ResourceModel[]>> GetResources([FromBody] GetResourcesRequest model)
        {
            return api.Group("ResourceGroup").Action<GetResourcesRequest, ResourceModel[]>("GetResources").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ResourceModel>> GetResource([FromBody] GetResourceGroupResourceRequest model)
        {
            return api.Group("ResourceGroup").Action<GetResourceGroupResourceRequest, ResourceModel>("GetResource").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppRoleModel[]>> GetRoleAccess([FromBody] GetResourceGroupRoleAccessRequest model)
        {
            return api.Group("ResourceGroup").Action<GetResourceGroupRoleAccessRequest, AppRoleModel[]>("GetRoleAccess").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] GetResourceGroupModCategoryRequest model)
        {
            return api.Group("ResourceGroup").Action<GetResourceGroupModCategoryRequest, ModifierCategoryModel>("GetModCategory").Execute(model);
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