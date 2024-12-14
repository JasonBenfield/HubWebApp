using XTI_HubWebAppApiActions.AppPublish;

// Generated Code
namespace XTI_HubWebAppApi.Publish;
public sealed partial class PublishGroup : AppApiGroupWrapper
{
    internal PublishGroup(AppApiGroup source, PublishGroupBuilder builder) : base(source)
    {
        BeginPublish = builder.BeginPublish.Build();
        EndPublish = builder.EndPublish.Build();
        GetVersions = builder.GetVersions.Build();
        NewVersion = builder.NewVersion.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<PublishVersionRequest, XtiVersionModel> BeginPublish { get; }
    public AppApiAction<PublishVersionRequest, XtiVersionModel> EndPublish { get; }
    public AppApiAction<AppKeyRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiAction<NewVersionRequest, XtiVersionModel> NewVersion { get; }
}