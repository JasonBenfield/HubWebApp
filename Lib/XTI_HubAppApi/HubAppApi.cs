﻿using System;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi
{
    public sealed partial class HubAppApi : WebAppApiWrapper
    {
        public HubAppApi
        (
            IAppApiUser user,
            IServiceProvider services
        )
            : base
            (
                new AppApi
                (
                    HubInfo.AppKey,
                    user,
                    ResourceAccess.AllowAuthenticated()
                        .WithAllowed(HubRoles.Instance.Admin)
                ),
                services
            )
        {
            auth(services);
            permanentLog(services);
            appList(services);
            appInquiry(services);
            appRegistration(services);
            appPublish(services);
            version(services);
            resourceGroup(services);
            resource(services);
            modCategory(services);
            userList(services);
            userInquiry(services);
            appUser(services);
            appUserMaintenance(services);
            userMaintenance(services);
        }

        partial void auth(IServiceProvider services);

        partial void userList(IServiceProvider services);

        partial void permanentLog(IServiceProvider services);

        partial void appList(IServiceProvider services);

        partial void appInquiry(IServiceProvider services);

        partial void appRegistration(IServiceProvider services);

        partial void appPublish(IServiceProvider services);

        partial void modCategory(IServiceProvider services);

        partial void resourceGroup(IServiceProvider services);

        partial void resource(IServiceProvider services);

        partial void appUser(IServiceProvider services);

        partial void appUserMaintenance(IServiceProvider services);

        partial void version(IServiceProvider services);

        partial void userInquiry(IServiceProvider services);

        partial void userMaintenance(IServiceProvider services);

    }
}
