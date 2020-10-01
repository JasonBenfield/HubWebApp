using XTI_App.Api;
using XTI_App;

namespace HubWebApp.AuthApi
{
    public abstract class AuthGroupFactory : IAuthGroupFactory
    {
        public AppAction<LoginModel, LoginResult> CreateAuthenticateAction(AppApiUser user)
        {
            var accessToken = CreateAccessTokenForAuthenticate();
            var auth = createAuthentication(accessToken);
            return new AuthenticateAction(auth);
        }

        public AppAction<LoginModel, LoginResult> CreateLoginAction(AppApiUser user)
        {
            var accessToken = CreateAccessTokenForLogin();
            var auth = createAuthentication(accessToken);
            return new LoginAction(auth);
        }

        private Authentication createAuthentication(AccessToken accessToken)
        {
            var unverifiedUser = CreateUnverifiedUser();
            var hashedPasswordFactory = CreateHashedPasswordFactory();
            return new Authentication(unverifiedUser, accessToken, hashedPasswordFactory);
        }

        protected abstract AccessToken CreateAccessTokenForAuthenticate();

        protected abstract AccessToken CreateAccessTokenForLogin();

        private UnverifiedUser CreateUnverifiedUser()
        {
            var appFactory = CreateAppFactory();
            return new UnverifiedUser(appFactory);
        }

        protected abstract AppFactory CreateAppFactory();

        protected abstract IHashedPasswordFactory CreateHashedPasswordFactory();

    }
}
