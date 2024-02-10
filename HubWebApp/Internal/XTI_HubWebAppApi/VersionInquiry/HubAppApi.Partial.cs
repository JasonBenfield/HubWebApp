using XTI_HubWebAppApi.VersionInquiry;

namespace XTI_HubWebAppApi;

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
                ResourceAccess.AllowAuthenticated().WithAllowed(HubInfo.Roles.AppViewerRoles)
            ),
            sp
        );
    }
}