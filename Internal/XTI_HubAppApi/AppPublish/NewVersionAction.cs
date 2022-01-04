using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppPublish;

public sealed class NewVersionAction : AppAction<NewVersionRequest, AppVersionModel>
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public NewVersionAction(AppFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<AppVersionModel> Execute(NewVersionRequest model)
    {
        var version = await appFactory.Apps.StartNewVersion
        (
            model.AppKey,
            model.Domain,
            model.VersionType,
            clock.Now()
        );
        return version.ToModel();
    }
}