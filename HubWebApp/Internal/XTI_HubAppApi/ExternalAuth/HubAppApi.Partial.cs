using XTI_Hub;
using XTI_HubAppApi.ExternalAuth;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private ExternalAuthGroup? externalAuth;

    public ExternalAuthGroup ExternalAuth
    {
        get => externalAuth ?? throw new ArgumentNullException(nameof(externalAuth));
    }

    partial void createExternalAuth(IServiceProvider services)
    {
        externalAuth = new ExternalAuthGroup
        (
            source.AddGroup
            (
                nameof(ExternalAuth),
                HubInfo.ModCategories.Apps,
                Access.WithAllowed(HubInfo.Roles.Authenticator)
            ),
            services
        );
    }
}