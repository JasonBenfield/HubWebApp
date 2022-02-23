using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppPublish;

internal sealed class NextVersionKeyAction : AppAction<EmptyRequest, AppVersionKey>
{
    private readonly AppFactory appFactory;

    public NextVersionKeyAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public Task<AppVersionKey> Execute(EmptyRequest model) => appFactory.Versions.NextKey();
}