using XTI_HubWebAppApi.ResourceGroupInquiry;

namespace XTI_HubWebAppApi;

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