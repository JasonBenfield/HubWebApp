using XTI_HubWebAppApiActions.ExternalAuth;

// Generated Code
namespace XTI_HubWebAppApi.ExternalAuth;
public sealed partial class ExternalAuthGroup : AppApiGroupWrapper
{
    internal ExternalAuthGroup(AppApiGroup source, ExternalAuthGroupBuilder builder) : base(source)
    {
        ExternalAuthKey = builder.ExternalAuthKey.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<ExternalAuthKeyModel, AuthenticatedLoginResult> ExternalAuthKey { get; }
}