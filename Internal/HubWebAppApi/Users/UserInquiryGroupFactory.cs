using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Abstractions;

namespace HubWebAppApi.Users
{
    public sealed class UserInquiryGroupFactory
    {
        private readonly IServiceProvider services;

        public UserInquiryGroupFactory(IServiceProvider services)
        {
            this.services = services;
        }

        internal GetUserAction CreateGetUser()
        {
            var factory = services.GetService<AppFactory>();
            return new GetUserAction(factory);
        }

        internal RedirectToAppUserAction CreateRedirectToAppUser()
        {
            var factory = services.GetService<AppFactory>();
            var path = services.GetService<XtiPath>();
            var hubApi = services.GetService<HubAppApi>();
            return new RedirectToAppUserAction(factory, path, hubApi);
        }
    }
}
