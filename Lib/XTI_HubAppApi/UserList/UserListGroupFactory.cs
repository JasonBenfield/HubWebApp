using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_WebApp;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.UserList
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

        internal GetSystemUsersAction CreateGetSystemUsers()
        {
            var factory = services.GetService<AppFactory>();
            return new GetSystemUsersAction(factory);
        }

        internal AddUserValidation CreateAddUserValidation()
        {
            return new AddUserValidation();
        }

        internal AddUserAction CreateAddUser()
        {
            var appFactory = services.GetService<AppFactory>();
            var pwdFactory = services.GetService<IHashedPasswordFactory>();
            var clock = services.GetService<Clock>();
            return new AddUserAction(appFactory, pwdFactory, clock);
        }
    }
}
