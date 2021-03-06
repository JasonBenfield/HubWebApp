﻿using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetResourceAction : AppAction<int, ResourceModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceModel> Execute(int resourceID)
        {
            var app = await appFromPath.Value();
            var resource = await app.Resource(resourceID);
            return resource.ToModel();
        }
    }
}
