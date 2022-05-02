namespace XTI_HubAppApi.VersionInquiry;

public sealed class VersionInquiryGroup : AppApiGroupWrapper
{
    public VersionInquiryGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        GetVersion = source.AddAction(actions.Action(nameof(GetVersion), () => sp.GetRequiredService<GetVersionAction>()));
        GetResourceGroup = source.AddAction(actions.Action(nameof(GetResourceGroup), () => sp.GetRequiredService<GetResourceGroupAction>()));
    }

    public AppApiAction<string, XtiVersionModel> GetVersion { get; }
    public AppApiAction<GetVersionResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
}