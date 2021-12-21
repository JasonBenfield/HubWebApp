using Microsoft.Extensions.Hosting;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class NewInstallationAction : AppAction<NewInstallationRequest, NewInstallationResult>
{
    private readonly IHostEnvironment hostEnv;
    private readonly AppFactory appFactory;
    private readonly IClock clock;

    public NewInstallationAction(IHostEnvironment hostEnv, AppFactory appFactory, IClock clock)
    {
        this.hostEnv = hostEnv;
        this.appFactory = appFactory;
        this.clock = clock;
    }

    public async Task<NewInstallationResult> Execute(NewInstallationRequest model)
    {
        var installationProcess = new InstallationProcess(appFactory);
        var result = await installationProcess.NewInstallation
        (
            model.AppKey,
            model.QualifiedMachineName,
            hostEnv.IsProduction(),
            clock.Now()
        );
        return result;
    }
}