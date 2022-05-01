using XTI_HubAppApi.Authenticators;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private AuthenticatorsGroup? authenticators;

    public AuthenticatorsGroup Authenticators
    {
        get => authenticators ?? throw new ArgumentNullException(nameof(authenticators));
    }

    partial void createAuthenticators(IServiceProvider sp)
    {
        authenticators = new AuthenticatorsGroup
        (
            source.AddGroup
            (
                nameof(Authenticators),
                HubInfo.ModCategories.Apps
            ),
            sp
        );
    }
}