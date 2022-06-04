﻿namespace XTI_HubAppApi.Auth;

public sealed class VerifyLoginFormAction : AppAction<EmptyRequest, WebPartialViewResult>
{
    public Task<WebPartialViewResult> Execute(EmptyRequest model, CancellationToken stoppingToken)
        => Task.FromResult(new WebPartialViewResult("VerifyLoginForm"));
}