using XTI_App.Abstractions;

namespace SupportSetupApp;

internal sealed class SupportAppSetup : IAppSetup
{
    public Task Run(AppVersionKey versionKey)
    {
        return Task.CompletedTask;
    }
}
