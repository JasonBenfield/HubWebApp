using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;
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

        public Task<IAppUser> User() => userContext.User();

        public Task<IAppUser> User(AppUserName userName) => userContext.User(userName);

        public Task<AppUserName> CurrentUserName()
        {
            var userName = getUserName();
            return Task.FromResult
            (
                string.IsNullOrWhiteSpace(userName) ? AppUserName.Anon : new AppUserName(userName)
            );
        }
    }
}
