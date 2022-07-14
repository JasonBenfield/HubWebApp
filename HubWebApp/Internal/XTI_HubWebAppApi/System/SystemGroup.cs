namespace XTI_HubWebAppApi.System;

public sealed class SystemGroup : AppApiGroupWrapper
{
    public SystemGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetAppContext = source.AddAction(nameof(GetAppContext), () => sp.GetRequiredService<GetAppContextAction>());
        GetUserContext = source.AddAction(nameof(GetUserContext), () => sp.GetRequiredService<GetUserContextAction>());
    }

    public AppApiAction<EmptyRequest, AppContextModel> GetAppContext { get; }
    public AppApiAction<GetUserContextRequest, UserContextModel> GetUserContext { get; }
}