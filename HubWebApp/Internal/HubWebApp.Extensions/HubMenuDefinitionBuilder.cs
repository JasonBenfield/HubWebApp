using XTI_App.Abstractions;
using XTI_HubWebAppApi;
using XTI_WebApp.Api;

namespace HubWebApp.Extensions;

internal sealed class HubMenuDefinitionBuilder : IMenuDefinitionBuilder
{
    private readonly UserMenuDefinition userMenuDefinition;

    public HubMenuDefinitionBuilder(UserMenuDefinition userMenuDefinition)
    {
        this.userMenuDefinition = userMenuDefinition;
    }

    public AppMenuDefinitions Build() =>
        new AppMenuDefinitions
        (
            userMenuDefinition.Value
        );
}


