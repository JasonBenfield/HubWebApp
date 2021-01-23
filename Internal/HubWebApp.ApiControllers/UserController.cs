// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_WebApp.Api;
using HubWebApp.Api;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class UserController : Controller
    {
        public UserController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        public async Task<IActionResult> Index(UserStartRequest model)
        {
            var result = await api.Group("User").Action<UserStartRequest, WebViewResult>("Index").Execute(model);
            return View(result.Data.ViewName);
        }
    }
}