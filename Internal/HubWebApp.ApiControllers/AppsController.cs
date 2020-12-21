// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XTI_App.Api;
using XTI_WebApp.Api;
using XTI_App;
using HubWebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class AppsController : Controller
    {
        public AppsController(HubAppApi api, XtiPath xtiPath)
        {
            this.api = api;
            this.xtiPath = xtiPath;
        }

        private readonly HubAppApi api;
        private readonly XtiPath xtiPath;
        public async Task<IActionResult> Index()
        {
            var result = await api.Group("Apps").Action<EmptyRequest, AppActionViewResult>("Index").Execute(xtiPath.Modifier, new EmptyRequest());
            return View(result.Data.ViewName);
        }

        [HttpPost]
        public Task<ResultContainer<AppModel[]>> All()
        {
            return api.Group("Apps").Action<EmptyRequest, AppModel[]>("All").Execute(xtiPath.Modifier, new EmptyRequest());
        }
    }
}