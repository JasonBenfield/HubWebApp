using XTI_App.Abstractions;

namespace AuthenticatorSetupApp;

internal sealed class AuthenticatorAppSetup : IAppSetup
{
    public Task Run(AppVersionKey versionKey)
    {
        return Task.CompletedTask;
    }
}
