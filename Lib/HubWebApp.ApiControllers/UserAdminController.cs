// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HubWebApp.UserAdminApi;
using HubWebApp.Api;
using XTI_App.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class UserAdminController : Controller
    {
        public UserAdminController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<int>> AddUser([FromBody] AddUserModel model)
        {
            return api.Group("UserAdmin").Action<AddUserModel, int>("AddUser").Execute(model);
        }
    }
}