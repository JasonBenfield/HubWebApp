using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App.Abstractions;
using XTI_App.Extensions;
using XTI_Hub;
using XTI_HubAppApi.Auth;
using XTI_TempLog;
using XTI_WebApp;

namespace XTI_HubAppApi
{
    internal static class AuthExtensions
    {
        public static void AddAuthGroupServices(this IServiceCollection services)
        {
            services.AddScoped<UnverifiedUser>();
            services.AddScoped(sp =>
            {
                var access = sp.GetService<AccessForAuthenticate>();
                var auth = createAuthentication(sp, access);
                return new AuthenticateAction(auth);
            });
            services.AddScoped(sp =>
            {
                var access = sp.GetService<AccessForLogin>();
                var auth = createAuthentication(sp,access);
                var anonClient = sp.GetService<IAnonClient>();
                return new LoginAction(auth, anonClient);
            });
            services.AddScoped<LogoutAction>();
            services.AddScoped<StartAction>();
            services.AddScoped<VerifyLoginAction>();
            services.AddScoped<VerifyLoginFormAction>();
        }

        private static Authentication createAuthentication(IServiceProvider sp, IAccess access)
        {
            var tempLogSession = sp.GetService<TempLogSession>();
            var unverifiedUser = new UnverifiedUser(sp.GetService<AppFactory>());
            var hashedPasswordFactory = sp.GetService<IHashedPasswordFactory>();
            var userContext = sp.GetService<CachedUserContext>();
            return new Authentication
            (
                tempLogSession,
                unverifiedUser,
                access,
                hashedPasswordFactory,
                userContext
            );
        }

    }
}
