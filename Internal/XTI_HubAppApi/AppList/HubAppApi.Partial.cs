using System;
using XTI_App.Api;
using XTI_HubAppApi.AppList;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        public AppListGroup Apps { get; private set; }

        partial void appList(IServiceProvider services)
        {
            Apps = new AppListGroup
            (
                source.AddGroup(nameof(Apps), ResourceAccess.AllowAuthenticated()),
                services
            );
        }
    }
}
