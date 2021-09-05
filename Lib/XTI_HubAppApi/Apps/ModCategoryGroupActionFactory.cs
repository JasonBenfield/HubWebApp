using Microsoft.Extensions.DependencyInjection;
using System;

namespace XTI_HubAppApi.Apps
{
    public sealed class ModCategoryGroupActionFactory
    {
        private readonly IServiceProvider services;

        public ModCategoryGroupActionFactory(IServiceProvider services)
        {
            this.services = services;
        }

        internal GetModCategoryAction CreateGetModCategory()
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetModCategoryAction(appFromPath);
        }

        internal GetModifiersAction CreateGetModifiers()
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetModifiersAction(appFromPath);
        }

        internal GetModCategoryResourceGroupsAction CreateGetResourceGroups()
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetModCategoryResourceGroupsAction(appFromPath);
        }
    }
}
