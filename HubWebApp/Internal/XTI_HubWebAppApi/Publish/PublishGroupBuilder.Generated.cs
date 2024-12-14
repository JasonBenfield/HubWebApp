using XTI_HubWebAppApiActions.AppPublish;

// Generated Code
namespace XTI_HubWebAppApi.Publish;
public sealed partial class PublishGroupBuilder
{
    private readonly AppApiGroup source;
    internal PublishGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        BeginPublish = source.AddAction<PublishVersionRequest, XtiVersionModel>("BeginPublish").WithExecution<BeginPublishAction>();
        EndPublish = source.AddAction<PublishVersionRequest, XtiVersionModel>("EndPublish").WithExecution<EndPublishAction>();
        GetVersions = source.AddAction<AppKeyRequest, XtiVersionModel[]>("GetVersions").WithExecution<GetVersionsAction>();
        NewVersion = source.AddAction<NewVersionRequest, XtiVersionModel>("NewVersion").WithExecution<NewVersionAction>().WithValidation<NewVersionValidation>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<PublishVersionRequest, XtiVersionModel> BeginPublish { get; }
    public AppApiActionBuilder<PublishVersionRequest, XtiVersionModel> EndPublish { get; }
    public AppApiActionBuilder<AppKeyRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiActionBuilder<NewVersionRequest, XtiVersionModel> NewVersion { get; }

    public PublishGroup Build() => new PublishGroup(source, this);
}