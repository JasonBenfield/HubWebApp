using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App;
using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;

namespace HubWebApp.UserApi
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
