// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XTI_App.Api;
using HubWebApp.AuthApi;
using HubWebApp.Api;
using XTI_App;

namespace HubWebApp.ApiControllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public AuthController(HubAppApi api, XtiPath xtiPath)
        {
            this.api = api;
            this.xtiPath = xtiPath;
        }

        private readonly HubAppApi api;
        private readonly XtiPath xtiPath;
        public async Task<IActionResult> Index()
        {
            var result = await api.Group("Auth").Action<EmptyRequest, AppActionViewResult>("Index").Execute(xtiPath.Modifier, new EmptyRequest());
            return View(result.Data.ViewName);
        }

        public async Task<IActionResult> Start(StartRequest model)
        {
            var result = await api.Group("Auth").Action<StartRequest, AppActionRedirectResult>("Start").Execute(xtiPath.Modifier, model);
            return Redirect(result.Data.Url);
        }

        [HttpPost]
        public Task<ResultContainer<LoginResult>> Login([FromBody] LoginModel model)
        {
            return api.Group("Auth").Action<LoginModel, LoginResult>("Login").Execute(xtiPath.Modifier, model);
        }
    }
}