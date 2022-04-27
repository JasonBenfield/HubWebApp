using XTI_AuthenticatorWebAppApi.Home;

namespace XTI_AuthenticatorWebAppApi;

partial class AuthenticatorAppApi
{
    private HomeGroup? home;

    public HomeGroup Home { get => home ?? throw new ArgumentNullException(nameof(home)); }

    partial void createHomeGroup(IServiceProvider sp)
    {
        home = new HomeGroup
        (
            source.AddGroup(nameof(Home), ResourceAccess.AllowAnonymous()),
            sp
        );
    }
}