namespace XTI_HubWebAppApi.ExternalAuth;

public sealed class ExternalAuthGroup : AppApiGroupWrapper
{
    public ExternalAuthGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        ExternalAuthKey = source.AddAction
        (
            nameof(ExternalAuthKey),
            () => sp.GetRequiredService<ExternalAuthKeyAction>()
        );
    }
    public AppApiAction<ExternalAuthKeyModel, AuthenticatedLoginResult> ExternalAuthKey { get; }
}