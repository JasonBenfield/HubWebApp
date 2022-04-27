namespace XTI_AuthenticatorWebAppApi;

public sealed class AuthenticatorAppApiFactory : AppApiFactory
{
    private readonly IServiceProvider sp;

    public AuthenticatorAppApiFactory(IServiceProvider sp)
    {
        this.sp = sp;
    }

    public new AuthenticatorAppApi Create(IAppApiUser user) => (AuthenticatorAppApi)base.Create(user);
    public new AuthenticatorAppApi CreateForSuperUser() => (AuthenticatorAppApi)base.CreateForSuperUser();

    protected override IAppApi _Create(IAppApiUser user) => new AuthenticatorAppApi(user, sp);
}