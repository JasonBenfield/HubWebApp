using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class InstallationUserCommand : ICommand
{
    private readonly Scopes scopes;

    public InstallationUserCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        var password = Guid.NewGuid().ToString();
        var user = await hubAdministration.AddOrUpdateInstallationUser(Environment.MachineName, password);
        var credentials = scopes.GetRequiredService<InstallationUserCredentials>();
        await credentials.Update
        (
            new CredentialValue
            (
                user.UserName,
                password
            )
        );
    }
}