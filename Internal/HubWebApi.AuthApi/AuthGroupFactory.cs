using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.AuthApi
{
    public sealed class AuthGroupFactory
    {
        private readonly IServiceProvider sp;

        public AuthGroupFactory(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public AppAction<LoginModel, LoginResult> CreateAuthenticateAction()
        {
            var access = sp.GetService<AccessForAuthenticate>();
            var auth = createAuthentication(access);
            return new AuthenticateAction(auth);
        }

        public AppAction<LoginModel, LoginResult> CreateLoginAction()
        {
            var access = sp.GetService<AccessForLogin>();
            var auth = createAuthentication(access);
            return new LoginAction(auth);
        }

        private Authentication createAuthentication(IAccess access)
        {
            var sesssionContext = sp.GetService<ISessionContext>();
            var unverifiedUser = new UnverifiedUser(sp.GetService<AppFactory>());
            var hashedPasswordFactory = sp.GetService<IHashedPasswordFactory>();
            return new Authentication(sesssionContext, unverifiedUser, access, hashedPasswordFactory);
        }

        public AppAction<EmptyRequest, AppActionRedirectResult> CreateLogoutAction()
        {
            var access = sp.GetService<AccessForLogin>();
            var sessionContext = sp.GetService<ISessionContext>();
            var clock = sp.GetService<Clock>();
            return new LogoutAction(access, sessionContext, clock);
        }
    }
}
