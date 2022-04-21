using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class RegisterAppAction : AppAction<RegisterAppRequest, AppWithModKeyModel>
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public RegisterAppAction(AppFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<AppWithModKeyModel> Execute(RegisterAppRequest model)
    {
        var appWithModifier = await new AppRegistration(appFactory, clock).Run
        (
            model.AppTemplate,
            model.Domain,
            model.VersionKey
        );
        return appWithModifier;
    }
}