using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class AddSystemUserCommand : ICommand
{
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly IHubAdministration hubAdmin;
    private readonly ISecretCredentialsFactory secretCredentialsFactory;
    private readonly AppVersionNameAccessor versionNameAccessor;

    public AddSystemUserCommand(SelectedAppKeys selectedAppKeys, IHubAdministration hubAdmin, ISecretCredentialsFactory secretCredentialsFactory, AppVersionNameAccessor versionNameAccessor)
    {
        this.selectedAppKeys = selectedAppKeys;
        this.hubAdmin = hubAdmin;
        this.secretCredentialsFactory = secretCredentialsFactory;
        this.versionNameAccessor = versionNameAccessor;
    }

    public async Task Execute(CancellationToken ct)
    {
        var appKeys = selectedAppKeys.Values
            .Where(a => !a.Type.Equals(AppType.Values.Package))
            .ToArray();
        var versionName = versionNameAccessor.Value;
        await hubAdmin.AddOrUpdateApps(versionName, appKeys, ct);
        foreach (var appKey in appKeys)
        {
            var password = Guid.NewGuid().ToString();
            var systemUser = await hubAdmin.AddOrUpdateSystemUser(appKey, Environment.MachineName, password, ct);
            var systemUserCredentials = new SystemUserCredentials(secretCredentialsFactory, appKey);
            await systemUserCredentials.Update(new CredentialValue(systemUser.UserName.Value, password));
        }
    }
}
