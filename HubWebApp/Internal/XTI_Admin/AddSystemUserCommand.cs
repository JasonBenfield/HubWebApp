using XTI_App.Abstractions;
using XTI_App.Secrets;
using XTI_Credentials;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class AddSystemUserCommand : ICommand
{
    private readonly Scopes scopes;

    public AddSystemUserCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var selectedAppKeys = scopes.GetRequiredService<SelectedAppKeys>();
        var appKeys = selectedAppKeys.Values.Where(a => !a.Type.Equals(AppType.Values.Package));
        var hubAdmin = scopes.GetRequiredService<IHubAdministration>();
        var options = scopes.GetRequiredService<AdminOptions>();
        var secretCredentialsFactory = scopes.GetRequiredService<ISecretCredentialsFactory>();
        var versionName = scopes.GetRequiredService<AppVersionNameAccessor>().Value;
        var appDefs = appKeys.Select(ak => new AppDefinitionModel(ak)).ToArray();
        await hubAdmin.AddOrUpdateApps(versionName, appDefs);
        foreach (var appKey in appKeys)
        {
            var password = Guid.NewGuid().ToString();
            var systemUser = await hubAdmin.AddOrUpdateSystemUser(appKey, Environment.MachineName, password);
            var systemUserCredentials = new SystemUserCredentials(secretCredentialsFactory, appKey);
            await systemUserCredentials.Update(new CredentialValue(systemUser.UserName.Value, password));
        }
    }
}
