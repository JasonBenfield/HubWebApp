using XTI_HubAppApi.VersionInquiry;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private VersionInquiryGroup? version;

    public VersionInquiryGroup Version
    {
        get=>version ?? throw new ArgumentNullException(nameof(version));
    }

    partial void createVersion(IServiceProvider sp)
    {
        version = new VersionInquiryGroup
        (
            source.AddGroup
            (
                nameof(Version),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.ViewApp)
            ),
            sp
        );
    }
}