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
                sp,
                AuthenticatorInfo.AppKey,
                user,
                serializedDefaultOptions
            ),
            sp
        )
    {
        createHomeGroup(sp);
    }

    partial void createHomeGroup(IServiceProvider sp);
}