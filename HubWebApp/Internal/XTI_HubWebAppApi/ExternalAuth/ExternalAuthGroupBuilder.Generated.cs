using XTI_HubWebAppApiActions.ExternalAuth;

// Generated Code
namespace XTI_HubWebAppApi.ExternalAuth;
public sealed partial class ExternalAuthGroupBuilder
{
    private readonly AppApiGroup source;
    internal ExternalAuthGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        ExternalAuthKey = source.AddAction<ExternalAuthKeyModel, AuthenticatedLoginResult>("ExternalAuthKey").WithExecution<ExternalAuthKeyAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<ExternalAuthKeyModel, AuthenticatedLoginResult> ExternalAuthKey { get; }

    public ExternalAuthGroup Build() => new ExternalAuthGroup(source, this);
}