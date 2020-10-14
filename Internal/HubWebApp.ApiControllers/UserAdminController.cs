// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XTI_App.Api;
using XTI_WebApp.Api;
using HubWebApp.UserAdminApi;
using HubWebApp.Api;
using XTI_App;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class UserAdminController : Controller
    {
        public UserAdminController(HubAppApi api, XtiPath xtiPath)
        {
            this.api = api;
            this.xtiPath = xtiPath;
        }

        private readonly HubAppApi api;
        private readonly XtiPath xtiPath;
        public async Task<IActionResult> Index()
        {
            var result = await api.Group("UserAdmin").Action<EmptyRequest, AppActionViewResult>("Index").Execute(xtiPath.Modifier, new EmptyRequest());
            return View(result.Data.ViewName);
        }

        [HttpPost]
        public Task<ResultContainer<int>> AddUser([FromBody] AddUserModel model)
        {
            return api.Group("UserAdmin").Action<AddUserModel, int>("AddUser").Execute(xtiPath.Modifier, model);
        }
    }
}