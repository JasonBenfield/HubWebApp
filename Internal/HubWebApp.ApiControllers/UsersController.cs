// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App.Api;
using XTI_App;
using HubWebAppApi.Users;
using HubWebAppApi;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public UsersController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        public async Task<IActionResult> Index()
        {
            var result = await api.Group("Users").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
            return View(result.Data.ViewName);
        }

        [HttpPost]
        public Task<ResultContainer<AppUserModel[]>> GetUsers()
        {
            return api.Group("Users").Action<EmptyRequest, AppUserModel[]>("GetUsers").Execute(new EmptyRequest());
        }

        [HttpPost]
        public Task<ResultContainer<int>> AddUser([FromBody] AddUserModel model)
        {
            return api.Group("Users").Action<AddUserModel, int>("AddUser").Execute(model);
        }
    }
}