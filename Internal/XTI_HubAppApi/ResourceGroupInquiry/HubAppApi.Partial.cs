using XTI_Hub;
using XTI_HubAppApi.ResourceGroupInquiry;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private ResourceGroupInquiryGroup? resourceGroup;

    public ResourceGroupInquiryGroup ResourceGroup
    {
        get => resourceGroup ?? throw new ArgumentNullException(nameof(resourceGroup));
    }

    partial void createResourceGroup(IServiceProvider services)
    {
        resourceGroup = new ResourceGroupInquiryGroup
        (
            source.AddGroup
            (
                nameof(ResourceGroup),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.ViewApp)
            ),
            services
        );
    }
}