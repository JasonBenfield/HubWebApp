// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_Hub;
using XTI_HubAppApi.VersionInquiry;
using XTI_HubAppApi;
using XTI_HubAppApi.UserMaintenance;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class VersionController : Controller
    {
        public VersionController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> GetVersion([FromBody] string model)
        {
            return api.Group("Version").Action<string, AppVersionModel>("GetVersion").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ResourceGroupModel>> GetResourceGroup([FromBody] GetVersionResourceGroupRequest model)
        {
            return api.Group("Version").Action<GetVersionResourceGroupRequest, ResourceGroupModel>("GetResourceGroup").Execute(model);
        }
    }
}