using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class AddAdminUserCommand : ICommand
{
    private readonly IHubAdministration hubAdmin;
    private readonly AdminOptions options;
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly ISecretCredentialsFactory secretCredentialsFactory;

    public AddAdminUserCommand(IHubAdministration hubAdmin, AdminOptions options, SelectedAppKeys selectedAppKeys, ISecretCredentialsFactory secretCredentialsFactory)
    {
        this.hubAdmin = hubAdmin;
        this.options = options;
        this.selectedAppKeys = selectedAppKeys;
        this.secretCredentialsFactory = secretCredentialsFactory;
    }

    public async Task Execute(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(options.UserName)) { throw new ArgumentException("UserName is required"); }
        if (string.IsNullOrWhiteSpace(options.Password)) { throw new ArgumentException("Password is required"); }
        var appKeys = selectedAppKeys.Values;
        foreach(var appKey in appKeys.Where(a => !a.Type.Equals(AppType.Values.Package)))
        {
            await hubAdmin.AddOrUpdateAdminUser
            (
                appKey, 
                new AppUserName(options.UserName), 
                options.Password,
                ct
            );
        }
        if (!string.IsNullOrWhiteSpace(options.CredentialKey))
        {
            var secretCredentials = secretCredentialsFactory.Create(options.CredentialKey);
            await secretCredentials.Update(options.UserName, options.Password);
        }
    }
}
