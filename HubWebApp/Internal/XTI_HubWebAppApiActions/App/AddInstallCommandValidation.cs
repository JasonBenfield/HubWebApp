using XTI_Core;

namespace XTI_HubWebAppApiActions.App;

public sealed class AddInstallCommandValidation : AppActionValidation<AddInstallCommandRequest>
{
    public Task Validate(ErrorList errors, AddInstallCommandRequest requestData, CancellationToken stoppingToken)
    {
        var versionKey = requestData.ToAppVersionKey();
        if (versionKey.IsNone())
        {
            errors.Add("Version Key is required.");
        }
        if(requestData.InstallConfigurationID <= 0)
        {
            errors.Add("Install Configuration ID is required.");
        }
        return Task.CompletedTask;
    }
}