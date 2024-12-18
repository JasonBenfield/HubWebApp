namespace XTI_HubWebAppApi.ExternalAuth;

partial class ExternalAuthGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.Authenticator);
    }
}
