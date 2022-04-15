using XTI_Credentials;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class ShowCredentialsCommand : ICommand
{
    private readonly Scopes scopes;

    public ShowCredentialsCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        if (string.IsNullOrWhiteSpace(options.CredentialKey)) { throw new ArgumentException("CredentialKey is required"); }
        var credentialFactory = scopes.GetRequiredService<ISecretCredentialsFactory>();
        var secretCredentials = credentialFactory.Create(options.CredentialKey);
        var credentials = await secretCredentials.Value();
        Console.WriteLine($"User Name: {credentials.UserName}");
        Console.WriteLine($"Password: {credentials.Password}");
    }
}
