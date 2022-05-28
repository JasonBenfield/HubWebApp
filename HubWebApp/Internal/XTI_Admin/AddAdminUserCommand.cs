using XTI_App.Abstractions;
using XTI_Credentials;
using XTI_Hub.Abstractions;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class AddAdminUserCommand : ICommand
{
    private readonly Scopes scopes;

    public AddAdminUserCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var hubAdmin = scopes.GetRequiredService<IHubAdministration>();
        var options = scopes.GetRequiredService<AdminOptions>();
        if (string.IsNullOrWhiteSpace(options.UserName)) { throw new ArgumentException("UserName is required"); }
        if (string.IsNullOrWhiteSpace(options.Password)) { throw new ArgumentException("Password is required"); }
        await hubAdmin.AddOrUpdateAdminUser(new AppUserName(options.UserName), options.Password);
        if (!string.IsNullOrWhiteSpace(options.CredentialKey))
        {
            var secretCredentialsFactory = scopes.GetRequiredService<ISecretCredentialsFactory>();
            var secretCredentials = secretCredentialsFactory.Create(options.CredentialKey);
            await secretCredentials.Update(options.UserName, options.Password);
        }
    }
}
