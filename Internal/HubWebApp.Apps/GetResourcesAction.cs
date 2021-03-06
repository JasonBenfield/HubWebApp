﻿using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetResourcesAction : AppAction<int, ResourceModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetResourcesAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceModel[]> Execute(int resourceGroupID)
        {
            var app = await appFromPath.Value();
            var resourceGroup = await app.ResourceGroup(resourceGroupID);
            var resources = await resourceGroup.Resources();
            return resources.Select(r => r.ToModel()).ToArray();
        }
    }
}
