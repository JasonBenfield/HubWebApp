// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_HubAppApi.AppRegistration;
using XTI_Hub;
using XTI_App.Api;
using XTI_App.Abstractions;
using XTI_HubAppApi;
using XTI_HubAppApi.Users;
using XTI_App;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class AppRegistrationController : Controller
    {
        public AppRegistrationController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> RegisterApp([FromBody] RegisterAppRequest model)
        {
            return api.Group("AppRegistration").Action<RegisterAppRequest, EmptyActionResult>("RegisterApp").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> NewVersion([FromBody] NewVersionRequest model)
        {
            return api.Group("AppRegistration").Action<NewVersionRequest, AppVersionModel>("NewVersion").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> BeginPublish([FromBody] GetVersionRequest model)
        {
            return api.Group("AppRegistration").Action<GetVersionRequest, AppVersionModel>("BeginPublish").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> EndPublish([FromBody] GetVersionRequest model)
        {
            return api.Group("AppRegistration").Action<GetVersionRequest, AppVersionModel>("EndPublish").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel[]>> GetVersions([FromBody] AppKey model)
        {
            return api.Group("AppRegistration").Action<AppKey, AppVersionModel[]>("GetVersions").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> GetVersion([FromBody] GetVersionRequest model)
        {
            return api.Group("AppRegistration").Action<GetVersionRequest, AppVersionModel>("GetVersion").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppUserModel>> AddSystemUser([FromBody] AddSystemUserRequest model)
        {
            return api.Group("AppRegistration").Action<AddSystemUserRequest, AppUserModel>("AddSystemUser").Execute(model);
        }
    }
}