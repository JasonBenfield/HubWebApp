namespace XTI_HubAppApi.ExternalAuth;

public sealed class ExternalAuthGroup : AppApiGroupWrapper
{
    public ExternalAuthGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        ExternalAuthKey = source.AddAction
        (
            actions.Action
            (
                nameof(ExternalAuthKey),
                () => sp.GetRequiredService<ExternalAuthKeyAction>()
            )
        );
    }
    public AppApiAction<ExternalAuthKeyModel, string> ExternalAuthKey { get; }
}