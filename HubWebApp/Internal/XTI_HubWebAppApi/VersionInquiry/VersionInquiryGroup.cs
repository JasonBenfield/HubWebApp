namespace XTI_HubWebAppApi.VersionInquiry;

public sealed class VersionInquiryGroup : AppApiGroupWrapper
{
    public VersionInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetVersion = source.AddAction(nameof(GetVersion), () => sp.GetRequiredService<GetVersionAction>());
    }

    public AppApiAction<string, XtiVersionModel> GetVersion { get; }
}