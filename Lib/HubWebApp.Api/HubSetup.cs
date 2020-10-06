﻿using HubWebApp.Core;
using System.Threading.Tasks;
using XTI_App;

namespace HubWebApp.Api
{
    public sealed class HubSetup
    {
        private readonly AppFactory appFactory;
        private readonly Clock clock;

        public HubSetup(AppFactory appFactory, Clock clock)
        {
            this.appFactory = appFactory;
            this.clock = clock;
        }

        public async Task Run()
        {
            var app = await appFactory.Apps().App(HubAppKey.Key);
            if (!app.Exists())
            {
                app = await appFactory.Apps().AddApp(HubAppKey.Key, clock.Now());
            }
            var currentVersion = await app.CurrentVersion();
            if (!currentVersion.IsCurrent())
            {
                currentVersion = await app.StartNewMajorVersion(clock.Now());
                await currentVersion.Published();
            }
            await app.SetRoles(HubRoles.Instance.Valus());
        }
    }
}
