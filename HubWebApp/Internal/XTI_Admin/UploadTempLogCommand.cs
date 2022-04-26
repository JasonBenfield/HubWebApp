using System.Text.Json;
using XTI_Core;
using XTI_PermanentLog;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_Admin;

internal sealed class UploadTempLogCommand : ICommand
{
    private readonly TempToPermanentLog tempToPermanent;

    public UploadTempLogCommand(Scopes scopes)
    {
        tempToPermanent = scopes.GetRequiredService<TempToPermanentLog>();
    }

    public Task Execute() => tempToPermanent.Move();
}
