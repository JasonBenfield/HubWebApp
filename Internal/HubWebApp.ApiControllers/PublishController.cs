// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_HubAppApi.AppPublish;
using XTI_App.Abstractions;
using XTI_Hub;
using XTI_HubAppApi;
using XTI_HubAppApi.UserMaintenance;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class PublishController : Controller
    {
        public PublishController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> NewVersion([FromBody] NewVersionRequest model)
        {
            return api.Group("Publish").Action<NewVersionRequest, AppVersionModel>("NewVersion").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> BeginPublish([FromBody] PublishVersionRequest model)
        {
            return api.Group("Publish").Action<PublishVersionRequest, AppVersionModel>("BeginPublish").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> EndPublish([FromBody] PublishVersionRequest model)
        {
            return api.Group("Publish").Action<PublishVersionRequest, AppVersionModel>("EndPublish").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel[]>> GetVersions([FromBody] AppKey model)
        {
            return api.Group("Publish").Action<AppKey, AppVersionModel[]>("GetVersions").Execute(model);
        }
    }
}