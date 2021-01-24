using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace HubWebApp.UserApi
{
    public sealed class UserListGroupFactory
    {
        private readonly IServiceProvider services;

        public UserListGroupFactory(IServiceProvider services)
        {
            this.services = services;
        }

        internal TitledViewAppAction<EmptyRequest> CreateIndex()
        {
            var pageContext = services.GetService<IPageContext>();
            return new TitledViewAppAction<EmptyRequest>(pageContext, "Index", "Users");
        }

        internal GetUsersAction CreateGetUsers()
        {
            var factory = services.GetService<AppFactory>();
            return new GetUsersAction(factory);
        }
    }
}
