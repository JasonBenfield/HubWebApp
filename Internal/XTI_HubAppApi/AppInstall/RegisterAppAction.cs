using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class RegisterAppAction : AppAction<RegisterAppRequest, EmptyActionResult>
{
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public RegisterAppAction(AppFactory appFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(RegisterAppRequest model)
    {
        var versionKey = string.IsNullOrWhiteSpace(model.VersionKey)
            ? AppVersionKey.Current
            : AppVersionKey.Parse(model.VersionKey);
        await new AppRegistration(appFactory, clock).Run
        (
            model.AppTemplate,
            versionKey,
            model.Versions
        );
        return new EmptyActionResult();
    }
}