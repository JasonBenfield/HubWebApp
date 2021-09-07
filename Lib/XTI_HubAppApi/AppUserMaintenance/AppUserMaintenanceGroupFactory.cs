using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App;

namespace XTI_HubAppApi.AppUserMaintenance
{
    public sealed class AppUserMaintenanceGroupFactory
    {
        private readonly IServiceProvider services;

        public AppUserMaintenanceGroupFactory(IServiceProvider services)
        {
            this.services = services;
        }

        internal AssignRoleAction CreateAssignRole()
        {
            var appFromPath = services.GetService<AppFromPath>();
            var appFactory = services.GetService<AppFactory>();
            return new AssignRoleAction(appFromPath, appFactory);
        }

        internal UnassignRoleAction CreateUnassignRole()
        {
            var appFromPath = services.GetService<AppFromPath>();
            var appFactory = services.GetService<AppFactory>();
            return new UnassignRoleAction(appFromPath, appFactory);
        }
    }
}
