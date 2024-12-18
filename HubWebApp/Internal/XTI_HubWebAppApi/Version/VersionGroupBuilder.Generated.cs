using XTI_HubWebAppApiActions.VersionInquiry;

// Generated Code
#nullable enable
namespace XTI_HubWebAppApi.Version;
public sealed partial class VersionGroupBuilder
{
    private readonly AppApiGroup source;
    internal VersionGroupBuilder(AppApiGroup source)
    {
        this.source = source;
        GetVersion = source.AddAction<string, XtiVersionModel>("GetVersion").WithExecution<GetVersionAction>();
        Configure();
    }

    partial void Configure();
    public AppApiActionBuilder<string, XtiVersionModel> GetVersion { get; }

    public VersionGroup Build() => new VersionGroup(source, this);
}