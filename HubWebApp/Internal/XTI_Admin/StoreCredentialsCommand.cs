using XTI_Credentials;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class StoreCredentialsCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly ISecretCredentialsFactory credentialFactory;

    public StoreCredentialsCommand(AdminOptions options, ISecretCredentialsFactory credentialFactory)
    {
        this.options = options;
        this.credentialFactory = credentialFactory;
    }

    public async Task Execute()
    {
        if (string.IsNullOrWhiteSpace(options.CredentialKey)) { throw new ArgumentException("CredentialKey is required"); }
        if (string.IsNullOrWhiteSpace(options.UserName)) { throw new ArgumentException("UserName is required"); }
        if (string.IsNullOrWhiteSpace(options.Password)) { throw new ArgumentException("Password is required"); }
        var secretCredentials = credentialFactory.Create(options.CredentialKey);
        await secretCredentials.Update(new CredentialValue(options.UserName, options.Password));
        Console.WriteLine($"Secrets stored for user {options.UserName}");
    }
}
