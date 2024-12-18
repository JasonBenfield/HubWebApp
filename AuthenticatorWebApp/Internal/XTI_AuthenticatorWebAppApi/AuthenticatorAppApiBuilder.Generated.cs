using XTI_AuthenticatorWebAppApi.Home;

// Generated Code
#nullable enable
namespace XTI_AuthenticatorWebAppApi;
public sealed partial class AuthenticatorAppApiBuilder
{
    private readonly AppApi source;
    private readonly IServiceProvider sp;
    public AuthenticatorAppApiBuilder(IServiceProvider sp, IAppApiUser user)
    {
        source = new AppApi(sp, AuthenticatorAppKey.Value, user);
        this.sp = sp;
        Home = new HomeGroupBuilder(source.AddGroup("Home"));
        Configure();
    }

    partial void Configure();
    public HomeGroupBuilder Home { get; }

    public AuthenticatorAppApi Build() => new AuthenticatorAppApi(source, this);
}