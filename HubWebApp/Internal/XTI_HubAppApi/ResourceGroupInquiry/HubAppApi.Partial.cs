using XTI_HubAppApi.ResourceGroupInquiry;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private ResourceGroupInquiryGroup? resourceGroup;

    public ResourceGroupInquiryGroup ResourceGroup
    {
        get => resourceGroup ?? throw new ArgumentNullException(nameof(resourceGroup));
    }

    partial void createResourceGroup(IServiceProvider sp)
    {
        resourceGroup = new ResourceGroupInquiryGroup
        (
            source.AddGroup
            (
                nameof(ResourceGroup),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.ViewApp)
            ),
            sp
        );
    }
}