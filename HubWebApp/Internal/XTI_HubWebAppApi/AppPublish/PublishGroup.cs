namespace XTI_HubWebAppApi.AppPublish;

public sealed class PublishGroup : AppApiGroupWrapper
{
    public PublishGroup(AppApiGroup source, IServiceProvider services)
        : base(source)
    {
        NewVersion = source.AddAction
        (
            nameof(NewVersion),
            () => services.GetRequiredService<NewVersionAction>(),
            () => services.GetRequiredService<NewVersionValidation>()
        );
        BeginPublish = source.AddAction(nameof(BeginPublish), () => services.GetRequiredService<BeginPublishAction>());
        EndPublish = source.AddAction(nameof(EndPublish), () => services.GetRequiredService<EndPublishAction>());
        GetVersions = source.AddAction(nameof(GetVersions), () => services.GetRequiredService<GetVersionsAction>());
    }

    public AppApiAction<NewVersionRequest, XtiVersionModel> NewVersion { get; }
    public AppApiAction<PublishVersionRequest, XtiVersionModel> BeginPublish { get; }
    public AppApiAction<PublishVersionRequest, XtiVersionModel> EndPublish { get; }
    public AppApiAction<AppKey, XtiVersionModel[]> GetVersions { get; }
}