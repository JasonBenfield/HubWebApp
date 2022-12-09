using XTI_HubWebAppApi.ExternalAuth;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private ExternalAuthGroup? externalAuth;

    public ExternalAuthGroup ExternalAuth
    {
        get => externalAuth ?? throw new ArgumentNullException(nameof(externalAuth));
    }

    partial void createExternalAuth(IServiceProvider sp)
    {
        externalAuth = new ExternalAuthGroup
        (
            source.AddGroup
            (
                nameof(ExternalAuth),
                Access.WithAllowed(HubInfo.Roles.Authenticator)
            ),
            sp
        );
    }
}