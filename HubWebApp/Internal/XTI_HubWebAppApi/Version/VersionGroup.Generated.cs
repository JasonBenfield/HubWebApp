using XTI_HubWebAppApiActions.VersionInquiry;

// Generated Code
namespace XTI_HubWebAppApi.Version;
public sealed partial class VersionGroup : AppApiGroupWrapper
{
    internal VersionGroup(AppApiGroup source, VersionGroupBuilder builder) : base(source)
    {
        GetVersion = builder.GetVersion.Build();
        Configure();
    }

    partial void Configure();
    public AppApiAction<string, XtiVersionModel> GetVersion { get; }
}