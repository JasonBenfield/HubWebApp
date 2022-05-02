using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_Credentials;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class StoreCredentialsCommand : ICommand
{
    private readonly Scopes scopes;

    public StoreCredentialsCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        if (string.IsNullOrWhiteSpace(options.CredentialKey)) { throw new ArgumentException("CredentialKey is required"); }
        if (string.IsNullOrWhiteSpace(options.UserName)) { throw new ArgumentException("UserName is required"); }
        if (string.IsNullOrWhiteSpace(options.Password)) { throw new ArgumentException("Password is required"); }
        var credentialFactory = scopes.GetRequiredService<ISecretCredentialsFactory>();
        var secretCredentials = credentialFactory.Create(options.CredentialKey);
        await secretCredentials.Update(new CredentialValue(options.UserName, options.Password));
        Console.WriteLine($"Secrets stored for user {options.UserName}");
    }
}
