// Generated Code
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HubWebApp.AuthApi;
using HubWebApp.Api;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.ApiControllers
{
    [AllowAnonymous]
    public class AuthApiController : Controller
    {
        public AuthApiController(HubAppApi api, XtiPath xtiPath)
        {
            this.api = api;
            this.xtiPath = xtiPath;
        }

        private readonly HubAppApi api;
        private readonly XtiPath xtiPath;
        [HttpPost]
        public Task<ResultContainer<LoginResult>> Authenticate([FromBody] LoginCredentials model)
        {
            return api.Group("AuthApi").Action<LoginCredentials, LoginResult>("Authenticate").Execute(xtiPath.Modifier, model);
        }
    }
}