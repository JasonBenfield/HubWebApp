using HubWebAppApi.Apps;
using HubWebAppApi.PermanentLog;
using HubWebAppApi.Users;
using System;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebAppApi
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
                ),
                services
            )
        {
            Auth = new AuthGroup
            (
                source.AddGroup(nameof(Auth), ResourceAccess.AllowAnonymous()),
                new AuthActionFactory(services)
            );
            AuthApi = new AuthApiGroup
            (
                source.AddGroup(nameof(AuthApi), ResourceAccess.AllowAnonymous()),
                new AuthActionFactory(services)
            );
            PermanentLog = new PermanentLogGroup
            (
                source.AddGroup(nameof(PermanentLog), ResourceAccess.AllowAuthenticated()),
                new PermanentLogGroupActionFactory(services)
            );
            Apps = new AppListGroup
            (
                source.AddGroup(nameof(Apps), ResourceAccess.AllowAuthenticated()),
                new AppListActionFactory(services)
            );
            App = new AppInquiryGroup
            (
                source.AddGroup
                (
                    nameof(App),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                new AppInquiryActionFactory(services)
            );
            ResourceGroup = new ResourceGroupInquiryGroup
            (
                source.AddGroup
                (
                    nameof(ResourceGroup),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                new ResourceGroupInquiryActionFactory(services)
            );
            Resource = new ResourceInquiryGroup
            (
                source.AddGroup
                (
                    nameof(Resource),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                new ResourceInquiryActionFactory(services)
            );
            ModCategory = new ModCategoryGroup
            (
                source.AddGroup
                (
                    nameof(ModCategory),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp)
                ),
                new ModCategoryGroupActionFactory(services)
            );
            Users = new UserListGroup
            (
                source.AddGroup(nameof(Users), Access.WithAllowed(HubInfo.Roles.ViewUser)),
                new UserListGroupFactory(services)
            );
            UserInquiry = new UserInquiryGroup
            (
                source.AddGroup(nameof(UserInquiry), Access.WithAllowed(HubInfo.Roles.ViewUser)),
                new UserInquiryGroupFactory(services)
            );
            UserMaintenance = new UserMaintenanceGroup
            (
                source.AddGroup
                (
                    nameof(UserMaintenance),
                    Access.WithAllowed(HubInfo.Roles.EditUser)
                ),
                new UserMaintenanceGroupFactory(services)
            );
            AppUser = new AppUserGroup
            (
                source.AddGroup
                (
                    nameof(AppUser),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.ViewApp, HubInfo.Roles.ViewUser)
                ),
                new AppUserGroupFactory(services)
            );
            AppUserMaintenance = new AppUserMaintenanceGroup
            (
                source.AddGroup
                (
                    nameof(AppUserMaintenance),
                    HubInfo.ModCategories.Apps,
                    Access.WithAllowed(HubInfo.Roles.EditUser)
                ),
                new AppUserMaintenanceGroupFactory(services)
            );
        }

        public AuthGroup Auth { get; }
        public AuthApiGroup AuthApi { get; }
        public PermanentLogGroup PermanentLog { get; }
        public AppListGroup Apps { get; }
        public AppInquiryGroup App { get; }
        public ResourceGroupInquiryGroup ResourceGroup { get; }
        public ResourceInquiryGroup Resource { get; }
        public ModCategoryGroup ModCategory { get; }
        public UserListGroup Users { get; }
        public UserInquiryGroup UserInquiry { get; }
        public UserMaintenanceGroup UserMaintenance { get; }
        public AppUserGroup AppUser { get; }
        public AppUserMaintenanceGroup AppUserMaintenance { get; }
    }
}
