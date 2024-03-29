﻿using XTI_Core;

namespace XTI_HubWebAppApi.Storage;

internal sealed class StoreObjectValidation : AppActionValidation<StoreObjectRequest>
{
    public Task Validate(ErrorList errors, StoreObjectRequest model, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(model.StorageName))
        {
            errors.Add(StorageErrors.StorageNameIsRequired);
        }
        if (string.IsNullOrWhiteSpace(model.Data))
        {
            errors.Add(StorageErrors.StorageDataIsRequired);
        }
        return Task.CompletedTask;
    }
}
