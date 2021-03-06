﻿using HubWebApp.Apps;
using HubWebApp.Core;
using HubWebApp.UserAdminApi;
using System;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.Api
{
    public sealed class HubAppApi : WebAppApiWrapper
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
                )
            )
        {
            UserAdmin = new UserAdminGroup
            (
                source.AddGroup
                (
                    nameof(UserAdmin),
                    ModifierCategoryName.Default,
                    Access
                ),
                new UserAdminActionFactory(services)
            );
            Apps = new AppListGroup
            (
                source.AddGroup
                (
                    nameof(Apps)
                ),
                new AppGroupActionFactory(services)
            );
            App = new AppInquiryGroup
            (
                source.AddGroup
                (
                    nameof(App),
                    HubInfo.ModCategories.Apps
                ),
                new AppGroupActionFactory(services)
            );
            ResourceGroup = new ResourceGroupInquiryGroup
            (
                source.AddGroup
                (
                    nameof(ResourceGroup),
                    HubInfo.ModCategories.Apps
                ),
                new ResourceGroupInquiryActionFactory(services)
            );
            Resource = new ResourceInquiryGroup
            (
                source.AddGroup
                (
                    nameof(Resource),
                    HubInfo.ModCategories.Apps
                ),
                new ResourceInquiryActionFactory(services)
            );
            ModCategory = new ModCategoryGroup
            (
                source.AddGroup
                (
                    nameof(ModCategory),
                    HubInfo.ModCategories.Apps
                ),
                new ModCategoryGroupActionFactory(services)
            );
        }

        public UserAdminGroup UserAdmin { get; }
        public AppListGroup Apps { get; }
        public AppInquiryGroup App { get; }
        public ResourceGroupInquiryGroup ResourceGroup { get; }
        public ResourceInquiryGroup Resource { get; }
        public ModCategoryGroup ModCategory { get; }
    }
}
