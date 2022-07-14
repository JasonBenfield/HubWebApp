namespace XTI_HubWebAppApi.VersionInquiry;

public sealed class VersionInquiryGroup : AppApiGroupWrapper
{
    public VersionInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        GetVersion = source.AddAction(nameof(GetVersion), () => sp.GetRequiredService<GetVersionAction>());
        GetResourceGroup = source.AddAction(nameof(GetResourceGroup), () => sp.GetRequiredService<GetResourceGroupAction>());
    }

    public AppApiAction<string, XtiVersionModel> GetVersion { get; }
    public AppApiAction<GetVersionResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
}