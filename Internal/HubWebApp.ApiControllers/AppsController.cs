// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App.Api;
using XTI_App;
using HubWebApp.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class AppsController : Controller
    {
        public AppsController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        public async Task<IActionResult> Index()
        {
            var result = await api.Group("Apps").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
            return View(result.Data.ViewName);
        }

        [HttpPost]
        public Task<ResultContainer<AppModel[]>> All()
        {
            return api.Group("Apps").Action<EmptyRequest, AppModel[]>("All").Execute(new EmptyRequest());
        }

        public async Task<IActionResult> RedirectToApp(int model)
        {
            var result = await api.Group("Apps").Action<int, WebRedirectResult>("RedirectToApp").Execute(model);
            return Redirect(result.Data.Url);
        }
    }
}