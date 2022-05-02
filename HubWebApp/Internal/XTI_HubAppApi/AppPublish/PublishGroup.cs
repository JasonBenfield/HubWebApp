namespace XTI_HubAppApi.AppPublish;

public sealed class PublishGroup : AppApiGroupWrapper
{
    public PublishGroup(AppApiGroup source, IServiceProvider services)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        NewVersion = source.AddAction
        (
            actions.Action
            (
                nameof(NewVersion),
                () => services.GetRequiredService<NewVersionValidation>(),
                () => services.GetRequiredService<NewVersionAction>()
            )
        );
        BeginPublish = source.AddAction(actions.Action(nameof(BeginPublish), () => services.GetRequiredService<BeginPublishAction>()));
        EndPublish = source.AddAction(actions.Action(nameof(EndPublish), () => services.GetRequiredService<EndPublishAction>()));
        GetVersions = source.AddAction(actions.Action(nameof(GetVersions), () => services.GetRequiredService<GetVersionsAction>()));
    }

    public AppApiAction<NewVersionRequest, XtiVersionModel> NewVersion { get; }
    public AppApiAction<PublishVersionRequest, XtiVersionModel> BeginPublish { get; }
    public AppApiAction<PublishVersionRequest, XtiVersionModel> EndPublish { get; }
    public AppApiAction<AppKey, XtiVersionModel[]> GetVersions { get; }
}