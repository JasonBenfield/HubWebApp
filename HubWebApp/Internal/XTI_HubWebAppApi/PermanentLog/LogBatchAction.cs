﻿using XTI_TempLog.Abstractions;

namespace XTI_HubWebAppApi.PermanentLog;

public sealed class LogBatchAction : AppAction<LogBatchModel, EmptyActionResult>
{
    private readonly PermanentLog permanentLog;

    public LogBatchAction(PermanentLog permanentLog)
    {
        this.permanentLog = permanentLog;
    }

    public async Task<EmptyActionResult> Execute(LogBatchModel model, CancellationToken stoppingToken)
    {
        await permanentLog.LogBatch(model);
        return new EmptyActionResult();
    }
}