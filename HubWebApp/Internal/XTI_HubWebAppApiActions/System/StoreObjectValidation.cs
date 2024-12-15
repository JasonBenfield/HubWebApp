﻿using XTI_Core;

namespace XTI_HubWebAppApiActions.System;

public sealed class StoreObjectValidation : AppActionValidation<StoreObjectRequest>
{
    public Task Validate(ErrorList errors, StoreObjectRequest storeRequest, CancellationToken stoppingToken)
    {
        storeRequest.Validate(errors);
        return Task.CompletedTask;
    }
}
