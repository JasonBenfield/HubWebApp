namespace XTI_HubWebAppApi.System;

partial class SystemGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(AppRoleName.System);
        GetModifier.ThrottleRequestLogging().For(5).Minutes();
        GetUserByUserName.ThrottleRequestLogging().For(5).Minutes();
        GetUserOrAnon.ThrottleRequestLogging().For(5).Minutes();
        GetUserRoles.ThrottleRequestLogging().For(5).Minutes();
    }
}
