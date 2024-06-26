﻿namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private AppUserInquiry.AppUserGroup? appUser;

    public AppUserInquiry.AppUserGroup AppUser { get => appUser ?? throw new ArgumentNullException(nameof(appUser)); }

    partial void createAppUser(IServiceProvider sp)
    {
        appUser = new AppUserInquiry.AppUserGroup
        (
            source.AddGroup
            (
                nameof(AppUser),
                HubInfo.ModCategories.UserGroups,
                new(HubInfo.Roles.UserViewerRoles)
            ),
            sp
        );
    }
}