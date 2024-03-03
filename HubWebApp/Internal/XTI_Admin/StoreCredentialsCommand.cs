using XTI_Credentials;
using XTI_Secrets;

namespace XTI_Admin;

internal sealed class StoreCredentialsCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly ISecretCredentialsFactory credentialFactory;
    private readonly RemoteCommandService remoteCommandService;

    public StoreCredentialsCommand(AdminOptions options, ISecretCredentialsFactory credentialFactory, RemoteCommandService remoteCommandService)
    {
        this.options = options;
        this.credentialFactory = credentialFactory;
        this.remoteCommandService = remoteCommandService;
    }

    public async Task Execute(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(options.CredentialKey)) { throw new ArgumentException("CredentialKey is required"); }
        if (string.IsNullOrWhiteSpace(options.UserName)) { throw new ArgumentException("UserName is required"); }
        if (string.IsNullOrWhiteSpace(options.Password)) { throw new ArgumentException("Password is required"); }
        if (string.IsNullOrWhiteSpace(options.DestinationMachine))
        {
            var secretCredentials = credentialFactory.Create(options.CredentialKey);
            await secretCredentials.Update(new CredentialValue(options.UserName, options.Password));
            Console.WriteLine($"Secrets stored for user {options.UserName}");
        }
        else
        {
            var remoteOptions = options.Copy();
            remoteOptions.DestinationMachine = "";
            await remoteCommandService.Run
            (
                options.DestinationMachine,
                CommandNames.StoreCredentials.ToString(),
                remoteOptions
            );
        }
    }
}
