namespace XTI_HubWebAppApi.System;

partial class SystemGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(AppRoleName.System);
    }
}
