using XTI_HubAppApi.AppUserInquiry;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private AppUserGroup? appUser;

    public AppUserGroup AppUser { get=>appUser ?? throw new ArgumentNullException(nameof(appUser)); }

    partial void createAppUser(IServiceProvider sp)
    {
        appUser = new AppUserGroup
        (
            source.AddGroup
            (
                nameof(AppUser),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.ViewApp, HubInfo.Roles.ViewUser)
            ),
            sp
        );
    }
}