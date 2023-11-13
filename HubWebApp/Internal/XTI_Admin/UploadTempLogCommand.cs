using XTI_Core;
using XTI_PermanentLog;

namespace XTI_Admin;

internal sealed class UploadTempLogCommand : ICommand
{
    private readonly TempToPermanentLog tempToPermanent;
    private readonly XtiEnvironment xtiEnv;
    private readonly HubDbTypeAccessor hubDbTypeAccessor;
    private readonly IHttpClientFactory httpClientFactory;
    private readonly AdminOptions options;

    public UploadTempLogCommand(Scopes scopes)
    {
        tempToPermanent = scopes.GetRequiredService<TempToPermanentLog>();
        xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        hubDbTypeAccessor = scopes.GetRequiredService<HubDbTypeAccessor>();
        httpClientFactory = scopes.GetRequiredService<IHttpClientFactory>();
        options = scopes.GetRequiredService<AdminOptions>();
    }

    public async Task Execute()
    {
        if (string.IsNullOrWhiteSpace(options.DestinationMachine))
        {
            await tempToPermanent.Move();
        }
        else
        {
            var remoteCommandService = new RemoteCommandService(xtiEnv, httpClientFactory);
            var dict = new Dictionary<string, string>
            {
                { "HubAdministrationType", hubDbTypeAccessor.Value.ToString() }
            };
            await remoteCommandService.Run
            (
                options.DestinationMachine, 
                CommandNames.UploadTempLog.ToString(), 
                dict
            );
        }
    }
}
