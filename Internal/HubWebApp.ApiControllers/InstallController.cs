// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_HubAppApi.AppInstall;
using XTI_Hub;
using XTI_App.Api;
using XTI_App.Abstractions;
using XTI_HubAppApi;
using XTI_HubAppApi.UserMaintenance;
using XTI_App;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class InstallController : Controller
    {
        public InstallController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> RegisterApp([FromBody] RegisterAppRequest model)
        {
            return api.Group("Install").Action<RegisterAppRequest, EmptyActionResult>("RegisterApp").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> GetVersion([FromBody] GetVersionRequest model)
        {
            return api.Group("Install").Action<GetVersionRequest, AppVersionModel>("GetVersion").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppUserModel>> AddSystemUser([FromBody] AddSystemUserRequest model)
        {
            return api.Group("Install").Action<AddSystemUserRequest, AppUserModel>("AddSystemUser").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<NewInstallationResult>> NewInstallation([FromBody] NewInstallationRequest model)
        {
            return api.Group("Install").Action<NewInstallationRequest, NewInstallationResult>("NewInstallation").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<int>> BeginCurrentInstallation([FromBody] BeginInstallationRequest model)
        {
            return api.Group("Install").Action<BeginInstallationRequest, int>("BeginCurrentInstallation").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<int>> BeginVersionInstallation([FromBody] BeginInstallationRequest model)
        {
            return api.Group("Install").Action<BeginInstallationRequest, int>("BeginVersionInstallation").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> Installed([FromBody] InstalledRequest model)
        {
            return api.Group("Install").Action<InstalledRequest, EmptyActionResult>("Installed").Execute(model);
        }
    }
}