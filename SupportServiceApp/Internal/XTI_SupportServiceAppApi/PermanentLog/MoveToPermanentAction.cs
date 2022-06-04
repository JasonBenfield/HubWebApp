using XTI_PermanentLog;

namespace XTI_SupportServiceAppApi.PermanentLog;

public sealed class MoveToPermanentAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly TempToPermanentLog tempToPerm;

    public MoveToPermanentAction(TempToPermanentLog tempToPerm)
    {
        this.tempToPerm = tempToPerm;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        await tempToPerm.Move();
        return new EmptyActionResult();
    }
}