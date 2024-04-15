using XTI_Core;
using XTI_PermanentLog;

namespace XTI_Admin;

internal sealed class UploadTempLogCommand : ICommand
{
    private readonly TempToPermanentLog tempToPermanent;
    private readonly AdminOptions options;
    private readonly RemoteCommandService remoteCommandService;

    public UploadTempLogCommand(TempToPermanentLog tempToPermanent, AdminOptions options, RemoteCommandService remoteCommandService)
    {
        this.tempToPermanent = tempToPermanent;
        this.options = options;
        this.remoteCommandService = remoteCommandService;
    }

    public async Task Execute(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(options.DestinationMachine))
        {
            await tempToPermanent.Move();
        }
        else
        {
            var remoteOptions = options.Copy();
            remoteOptions.DestinationMachine = "";
            await remoteCommandService.Run
            (
                options.DestinationMachine,
                CommandNames.UploadTempLog.ToString(),
                remoteOptions
            );
        }
    }
}
