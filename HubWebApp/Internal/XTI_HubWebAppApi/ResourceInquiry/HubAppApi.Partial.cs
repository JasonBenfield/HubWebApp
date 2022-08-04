using XTI_HubWebAppApi.ResourceInquiry;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private ResourceInquiryGroup? resource;

    public ResourceInquiryGroup Resource
    {
        get => resource ?? throw new ArgumentNullException(nameof(resource));
    }

    partial void createResource(IServiceProvider sp)
    {
        resource = new ResourceInquiryGroup
        (
            source.AddGroup
            (
                nameof(Resource),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.ViewApp)
            ),
            sp
        );
    }
}