using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.UserInquiry
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

        internal GetUserByUserNameAction CreateGetUserByUserName()
        {
            var factory = services.GetService<AppFactory>();
            return new GetUserByUserNameAction(factory);
        }

        internal GetCurrentUserAction CreateGetCurrentUser()
        {
            var factory = services.GetService<AppFactory>();
            var userContext = services.GetService<IUserContext>();
            return new GetCurrentUserAction(factory, userContext);
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
