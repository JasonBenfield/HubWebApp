using HubWebApp.AuthApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;

namespace HubWebApp.Extensions
{
    public sealed class DiAuthGroupFactory : AuthGroupFactory
    {
        public DiAuthGroupFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        private readonly IServiceProvider sp;

        protected override AccessToken CreateAccessTokenForAuthenticate()
        {
            return sp.GetService<JwtAccessToken>();
        }

        protected override AccessToken CreateAccessTokenForLogin()
        {
            return sp.GetService<CookieAccessToken>();
        }

        protected override AppFactory CreateAppFactory()
        {
            return sp.GetService<AppFactory>();
        }

        protected override IHashedPasswordFactory CreateHashedPasswordFactory()
        {
            return sp.GetService<IHashedPasswordFactory>();
        }
    }
}
