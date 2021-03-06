﻿using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;

namespace HubWebApp.Apps
{
    public sealed class GetMostRecentErrorEventsForResourceGroupAction : AppAction<GetResourceGroupLogRequest, AppEventModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetMostRecentErrorEventsForResourceGroupAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppEventModel[]> Execute(GetResourceGroupLogRequest model)
        {
            var app = await appFromPath.Value();
            var group = await app.ResourceGroup(model.GroupID);
            var events = await group.MostRecentErrorEvents(model.HowMany);
            return events.Select(evt => evt.ToModel()).ToArray();
        }
    }
}
