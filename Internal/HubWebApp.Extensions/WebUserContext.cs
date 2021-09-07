using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_App.EfApi;
using XTI_WebApp;

namespace HubWebApp.Extensions
{
    public sealed class WebUserContext : ISourceUserContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DefaultUserContext userContext;

        public WebUserContext(IHttpContextAccessor httpContextAccessor, AppFactory appFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            userContext = new DefaultUserContext(appFactory, getUserName);
        }

        private string getUserName()
        {
            var xtiClaims = new XtiClaims(httpContextAccessor);
            return xtiClaims.UserName().Value;
        }

        public Task<string> GetKey() => Task.FromResult(getUserName());

        public Task<IAppUser> User() => userContext.User();

        public Task<IAppUser> User(AppUserName userName) => userContext.User(userName);
    }
}
