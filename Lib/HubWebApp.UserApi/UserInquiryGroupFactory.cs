using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;

namespace HubWebApp.UserApi
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
    }
}
