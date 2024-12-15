using XTI_HubWebAppApiActions;

namespace XTI_HubWebAppApi.Auth;

partial class AuthGroupBuilder
{
    partial void Configure()
    {
        source.AllowAnonymousAccess();
        LoginReturnKey.ResetAccessWithAllowed(HubInfo.Roles.Admin, HubInfo.Roles.System);
    }
}
