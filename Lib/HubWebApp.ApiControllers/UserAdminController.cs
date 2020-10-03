// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HubWebApp.UserAdminApi;
using HubWebApp.Api;
using XTI_App;
using XTI_App.Api;

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
        [HttpPost]
        public Task<ResultContainer<int>> AddUser([FromBody] AddUserModel model)
        {
            return api.Group("UserAdmin").Action<AddUserModel, int>("AddUser").Execute(xtiPath.Modifier, model);
        }
    }
}