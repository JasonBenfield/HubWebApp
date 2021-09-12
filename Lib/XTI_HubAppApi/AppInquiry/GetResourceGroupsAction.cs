﻿using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Api;

namespace XTI_HubAppApi.AppInquiry
{
    public sealed class GetResourceGroupsAction : AppAction<EmptyRequest, ResourceGroupModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceGroupsAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceGroupModel[]> Execute(EmptyRequest model)
        {
            var app = await appFromPath.Value();
            var version = await app.CurrentVersion();
            var resourceGroups = await version.ResourceGroups();
            return resourceGroups.Select(rg => rg.ToModel()).ToArray();
        }
    }
}
