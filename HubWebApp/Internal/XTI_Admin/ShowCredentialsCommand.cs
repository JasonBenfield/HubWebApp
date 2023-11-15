using XTI_Secrets;

namespace XTI_Admin;

internal sealed class ShowCredentialsCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly ISecretCredentialsFactory credentialFactory;

    public ShowCredentialsCommand(AdminOptions options, ISecretCredentialsFactory credentialFactory)
    {
        this.options = options;
        this.credentialFactory = credentialFactory;
    }

    public async Task Execute()
    {
        if (string.IsNullOrWhiteSpace(options.CredentialKey)) { throw new ArgumentException("CredentialKey is required"); }
        var secretCredentials = credentialFactory.Create(options.CredentialKey);
        var credentials = await secretCredentials.Value();
        Console.WriteLine($"User Name: {credentials.UserName}");
        Console.WriteLine($"Password: {credentials.Password}");
    }
}
