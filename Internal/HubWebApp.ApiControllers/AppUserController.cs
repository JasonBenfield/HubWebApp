// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App;
using HubWebAppApi.Users;
using HubWebAppApi;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class AppUserController : Controller
    {
        public AppUserController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        public async Task<IActionResult> Index(int model)
        {
            var result = await api.Group("AppUser").Action<int, WebViewResult>("Index").Execute(model);
            return View(result.Data.ViewName);
        }

        [HttpPost]
        public Task<ResultContainer<AppUserRoleModel[]>> GetUserRoles([FromBody] int model)
        {
            return api.Group("AppUser").Action<int, AppUserRoleModel[]>("GetUserRoles").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<UserRoleAccessModel>> GetUserRoleAccess([FromBody] int model)
        {
            return api.Group("AppUser").Action<int, UserRoleAccessModel>("GetUserRoleAccess").Execute(model);
        }
    }
}