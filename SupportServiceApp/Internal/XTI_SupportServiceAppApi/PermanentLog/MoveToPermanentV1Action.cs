using XTI_PermanentLog;

namespace XTI_SupportServiceAppApi.PermanentLog;

public sealed class MoveToPermanentV1Action : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly TempToPermanentLogV1 tempToPermV1;

    public MoveToPermanentV1Action(TempToPermanentLogV1 tempToPermV1)
    {
        this.tempToPermV1 = tempToPermV1;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
    {
        await tempToPermV1.Move();
        return new EmptyActionResult();
    }
}