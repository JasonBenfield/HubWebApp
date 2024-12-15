using XTI_HubWebAppApiActions.AuthApi;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.AuthApi;
public sealed partial class AuthApiGroupBuilder
{
    private readonly AppApiGroup source;
    internal AuthApiGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        Authenticate = source.AddAction<AuthenticateRequest, LoginResult>("Authenticate").WithExecution<AuthenticateAction>().WithValidation<AuthenticateValidation>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<AuthenticateRequest, LoginResult> Authenticate { get; }

    public AuthApiGroup Build() => new AuthApiGroup(source, this);
}