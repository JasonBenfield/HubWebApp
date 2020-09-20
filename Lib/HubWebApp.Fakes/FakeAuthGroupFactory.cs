using HubWebApp.AuthApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;

namespace HubWebApp.Fakes
{
    public sealed class FakeAuthGroupFactory : AuthGroupFactory
    {
        public FakeAuthGroupFactory(IServiceProvider sp)
        {
            this.sp = sp;
            AccessToken = new FakeAccessToken();
        }

        private readonly IServiceProvider sp;

        public FakeAccessToken AccessToken { get; }

        protected override AccessToken CreateAccessTokenForAuthenticate()
        {
            return AccessToken;
        }

        protected override AccessToken CreateAccessTokenForLogin()
        {
            return AccessToken;
        }

        protected override AppFactory CreateAppFactory() => sp.GetService<AppFactory>();

        protected override IHashedPasswordFactory CreateHashedPasswordFactory() => sp.GetService<IHashedPasswordFactory>();
    }
}
