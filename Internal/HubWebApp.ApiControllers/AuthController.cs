// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App.Api;
using XTI_HubAppApi.Auth;
using XTI_HubAppApi;
using XTI_HubAppApi.UserMaintenance;
using XTI_App;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public AuthController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        public async Task<IActionResult> Index()
        {
            var result = await api.Group("Auth").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
            return View(result.Data.ViewName);
        }

        [HttpPost]
        public Task<ResultContainer<EmptyActionResult>> VerifyLogin([FromBody] VerifyLoginForm model)
        {
            return api.Group("Auth").Action<VerifyLoginForm, EmptyActionResult>("VerifyLogin").Execute(model);
        }

        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> VerifyLoginForm()
        {
            var result = await api.Group("Auth").Action<EmptyRequest, WebPartialViewResult>("VerifyLoginForm").Execute(new EmptyRequest());
            return PartialView(result.Data.ViewName);
        }

        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await api.Group("Auth").Action<LoginModel, WebRedirectResult>("Login").Execute(model);
            return Redirect(result.Data.Url);
        }

        public async Task<IActionResult> Logout()
        {
            var result = await api.Group("Auth").Action<EmptyRequest, WebRedirectResult>("Logout").Execute(new EmptyRequest());
            return Redirect(result.Data.Url);
        }
    }
}