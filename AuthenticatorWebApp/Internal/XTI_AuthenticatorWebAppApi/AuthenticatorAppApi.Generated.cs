using XTI_AuthenticatorWebAppApi.Home;

// Generated Code
#nullable enable
namespace XTI_AuthenticatorWebAppApi;
public sealed partial class AuthenticatorAppApi : WebAppApiWrapper
{
    internal AuthenticatorAppApi(AppApi source, AuthenticatorAppApiBuilder builder) : base(source)
    {
        Home = builder.Home.Build();
        Configure();
    }

    partial void Configure();
    public HomeGroup Home { get; }
}