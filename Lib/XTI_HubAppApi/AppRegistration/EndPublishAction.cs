﻿using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppRegistration
{
    public sealed class EndPublishAction : AppAction<GetVersionRequest, AppVersionModel>
    {
        private readonly AppFactory appFactory;

        public EndPublishAction(AppFactory appFactory)
        {
            this.appFactory = appFactory;
        }

        public async Task<AppVersionModel> Execute(GetVersionRequest model)
        {
            var app = await appFactory.Apps().App(model.AppKey);
            var version = await app.Version(model.VersionKey);
            await version.Published();
            return version.ToModel();
        }
    }
}
