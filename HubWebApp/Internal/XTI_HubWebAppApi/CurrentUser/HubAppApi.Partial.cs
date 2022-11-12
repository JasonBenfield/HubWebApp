using XTI_HubWebAppApi.CurrentUser;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private CurrentUserGroup? _CurrentUser;

    public CurrentUserGroup CurrentUser { get => _CurrentUser ?? throw new ArgumentNullException(nameof(_CurrentUser)); }

    partial void createCurrentUserGroup(IServiceProvider sp)
    {
        _CurrentUser = new CurrentUserGroup
        (
            source.AddGroup(nameof(CurrentUser), ResourceAccess.AllowAuthenticated()),
            sp
        );
    }
}