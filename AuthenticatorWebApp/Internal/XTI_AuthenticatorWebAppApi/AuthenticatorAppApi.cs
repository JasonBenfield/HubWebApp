namespace XTI_AuthenticatorWebAppApi;

public sealed partial class AuthenticatorAppApi : WebAppApiWrapper
{
    public AuthenticatorAppApi
    (
        IAppApiUser user,
        string serializedDefaultOptions,
        IServiceProvider sp
    )
        : base
        (
            new AppApi
            (
                AuthenticatorInfo.AppKey,
                user,
                ResourceAccess.AllowAuthenticated()
                    .WithAllowed(AppRoleName.Admin),
                serializedDefaultOptions
            ),
            sp
        )
    {
        createHomeGroup(sp);
    }

    partial void createHomeGroup(IServiceProvider sp);
}