using XTI_App.Abstractions;
using XTI_HubWebAppApi;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions;

internal sealed class HubMenuDefinitionBuilder : IMenuDefinitionBuilder
{
    private readonly UserMenuDefinition userMenuDefinition;
    private readonly HubAppApi api;

    public HubMenuDefinitionBuilder(UserMenuDefinition userMenuDefinition, HubAppApi api)
    {
        this.userMenuDefinition = userMenuDefinition;
        this.api = api;
    }

    public AppMenuDefinitions Build() =>
        new AppMenuDefinitions
        (
            userMenuDefinition.Value,
            new MenuDefinition
            (
                "main",
                new LinkModel("apps", "Apps", api.Apps.Index.Path.RootPath()),
                new LinkModel("userGroups", "User Groups", api.UserGroups.Index.Path.RootPath()),
                new LinkModel("userRoles", "User Roles", api.UserRoles.Index.Path.RootPath()),
                new LinkModel("sessionLog", "Session Log", api.Logs.Sessions.Path.RootPath()),
                new LinkModel("accessLog", "Access Log", api.Logs.AppRequests.Path.RootPath()),
                new LinkModel("eventLog", "Event Log", api.Logs.LogEntries.Path.RootPath()),
                new LinkModel("installations", "Installations", api.Installations.Index.Path.RootPath())
            )
        );
}


