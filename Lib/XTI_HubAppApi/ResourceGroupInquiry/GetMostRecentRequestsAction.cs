﻿using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceGroupInquiry
{
    public sealed class GetMostRecentRequestsAction : AppAction<GetResourceGroupLogRequest, AppRequestExpandedModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentRequestsAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRequestExpandedModel[]> Execute(GetResourceGroupLogRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var group = await version.ResourceGroup(model.GroupID);
            var requests = await group.MostRecentRequests(model.HowMany);
            return requests.ToArray();
        }
    }
}