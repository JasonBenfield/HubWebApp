using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class AddInstallationUserCommand : ICommand
{
    private readonly IHubAdministration hubAdministration;
    private readonly InstallationUserCredentials credentials;

    public AddInstallationUserCommand(IHubAdministration hubAdministration, InstallationUserCredentials credentials)
    {
        this.hubAdministration = hubAdministration;
        this.credentials = credentials;
    }

    public async Task Execute()
    {
        var password = Guid.NewGuid().ToString();
        var user = await hubAdministration.AddOrUpdateInstallationUser(Environment.MachineName, password);
        await credentials.Update
        (
            new CredentialValue
            (
                user.UserName.Value,
                password
            )
        );
    }
}