// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App;
using HubWebApp.Api;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class ModCategoryController : Controller
    {
        public ModCategoryController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<ModifierCategoryModel>> GetModCategory([FromBody] int model)
        {
            return api.Group("ModCategory").Action<int, ModifierCategoryModel>("GetModCategory").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ModifierModel[]>> GetModifiers([FromBody] int model)
        {
            return api.Group("ModCategory").Action<int, ModifierModel[]>("GetModifiers").Execute(model);
        }

        [HttpPost]
        public Task<ResultContainer<ResourceGroupModel[]>> GetResourceGroups([FromBody] int model)
        {
            return api.Group("ModCategory").Action<int, ResourceGroupModel[]>("GetResourceGroups").Execute(model);
        }
    }
}