using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class RegisterAppAction : AppAction<RegisterAppRequest, AppWithModKeyModel>
{
    private readonly AppFactory appFactory;

    public RegisterAppAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<AppWithModKeyModel> Execute(RegisterAppRequest model)
    {
        var appWithModifier = await new AppRegistration(appFactory).Run
        (
            model.AppTemplate,
            model.VersionKey
        );
        return appWithModifier;
    }
}