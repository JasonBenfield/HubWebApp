using XTI_HubWebAppApiActions.AuthApi;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.AuthApi;
public sealed partial class AuthApiGroup : AppApiGroupWrapper
{
    internal AuthApiGroup(AppApiGroup source, AuthApiGroupBuilder builder) : base(source)
    {
        Authenticate = builder.Authenticate.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<AuthenticateRequest, LoginResult> Authenticate { get; }
}