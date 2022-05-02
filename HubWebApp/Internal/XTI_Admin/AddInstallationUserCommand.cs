using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class AddInstallationUserCommand : ICommand
{
    private readonly Scopes scopes;

    public AddInstallationUserCommand(Scopes scopes)
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