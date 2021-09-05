// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using XTI_HubAppApi;
using XTI_HubAppApi.Users;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [AllowAnonymous]
    public class AuthApiController : Controller
    {
        public AuthApiController(HubAppApi api)
        {
            this.api = api;
        }

        private readonly HubAppApi api;
        [HttpPost]
        public Task<ResultContainer<LoginResult>> Authenticate([FromBody] LoginCredentials model)
        {
            return api.Group("AuthApi").Action<LoginCredentials, LoginResult>("Authenticate").Execute(model);
        }
    }
}