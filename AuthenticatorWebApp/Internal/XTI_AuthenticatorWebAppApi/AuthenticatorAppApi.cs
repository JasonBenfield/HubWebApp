namespace XTI_AuthenticatorWebAppApi;

public sealed partial class AuthenticatorAppApi : WebAppApiWrapper
{
    public AuthenticatorAppApi
    (
        IAppApiUser user,
        IServiceProvider sp
    )
        : base
        (
            new AppApi
            (
                AuthenticatorInfo.AppKey,
                user,
                ResourceAccess.AllowAuthenticated()
                    .WithAllowed(AppRoleName.Admin)
            ),
            sp
        )
    {
        createHomeGroup(sp);
    }

    partial void createHomeGroup(IServiceProvider sp);
}