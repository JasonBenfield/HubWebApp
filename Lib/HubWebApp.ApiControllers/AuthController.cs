// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XTI_App.Api;
using HubWebApp.AuthApi;
using HubWebApp.Api;

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
            var result = await api.Group("Auth").Action<EmptyRequest, AppActionViewResult>("Index").Execute(new EmptyRequest());
            return View(result.Data.ViewName);
        }

        public async Task<IActionResult> Start(StartRequest model)
        {
            var result = await api.Group("Auth").Action<StartRequest, AppActionRedirectResult>("Start").Execute(model);
            return Redirect(result.Data.Url);
        }

        [HttpPost]
        public Task<ResultContainer<LoginResult>> Login([FromBody] LoginModel model)
        {
            return api.Group("Auth").Action<LoginModel, LoginResult>("Login").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<LoginResult>> Authenticate([FromBody] LoginModel model)
        {
            return api.Group("Auth").Action<LoginModel, LoginResult>("Authenticate").Execute(model);
        }
    }
}