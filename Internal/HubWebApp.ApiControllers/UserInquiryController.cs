// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_App;
using XTI_HubAppApi.Users;
using XTI_HubAppApi;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [Authorize]
    public class UserInquiryController : Controller
    {
        public UserInquiryController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<AppUserModel>> GetUser([FromBody] int model)
        {
            return api.Group("UserInquiry").Action<int, AppUserModel>("GetUser").Execute(model);
        }

        public async Task<IActionResult> RedirectToAppUser(RedirectToAppUserRequest model)
        {
            var result = await api.Group("UserInquiry").Action<RedirectToAppUserRequest, WebRedirectResult>("RedirectToAppUser").Execute(model);
            return Redirect(result.Data.Url);
        }
    }
}