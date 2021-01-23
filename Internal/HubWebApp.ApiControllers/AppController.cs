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
    public class AppController : Controller
    {
        public AppController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        public async Task<IActionResult> Index()
        {
            var result = await api.Group("App").Action<EmptyRequest, WebViewResult>("Index").Execute(new EmptyRequest());
            return View(result.Data.ViewName);
        }

        [HttpPost]
        public Task<ResultContainer<AppModel>> GetApp()
        {
            return api.Group("App").Action<EmptyRequest, AppModel>("GetApp").Execute(new EmptyRequest());
        }

        [HttpPost]
        public Task<ResultContainer<AppVersionModel>> GetCurrentVersion()
        {
            return api.Group("App").Action<EmptyRequest, AppVersionModel>("GetCurrentVersion").Execute(new EmptyRequest());
        }

        [HttpPost]
        public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups()
        {
            return api.Group("App").Action<EmptyRequest, ResourceGroupModel[]>("GetResourceGroups").Execute(new EmptyRequest());
        }

        [HttpPost]
        public Task<ResultContainer<AppRequestExpandedModel[]>> GetMostRecentRequests([FromBody] int model)
        {
            return api.Group("App").Action<int, AppRequestExpandedModel[]>("GetMostRecentRequests").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<AppEventModel[]>> GetMostRecentErrorEvents([FromBody] int model)
        {
            return api.Group("App").Action<int, AppEventModel[]>("GetMostRecentErrorEvents").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ModifierCategoryModel[]>> GetModifierCategories()
        {
            return api.Group("App").Action<EmptyRequest, ModifierCategoryModel[]>("GetModifierCategories").Execute(new EmptyRequest());
        }
    }
}