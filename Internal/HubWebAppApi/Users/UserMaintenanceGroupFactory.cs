using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;

namespace HubWebAppApi.Users
{
    public sealed class UserMaintenanceGroupFactory
    {
        private readonly IServiceProvider services;

        public UserMaintenanceGroupFactory(IServiceProvider services)
        {
            this.services = services;
        }

        internal EditUserAction CreateEditUser()
        {
            var factory = services.GetService<AppFactory>();
            return new EditUserAction(factory);
        }

        internal GetUserForEditAction CreateGetUserForEdit()
        {
            var factory = services.GetService<AppFactory>();
            return new GetUserForEditAction(factory);
        }
    }
}
