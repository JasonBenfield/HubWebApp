using XTI_Core;

namespace XTI_HubWebAppApiActions.Installations;

public sealed class RequestInstallationValidation : AppActionValidation<AddAppInstallCommandRequest>
{
    public Task Validate(ErrorList errors, AddAppInstallCommandRequest requestData, CancellationToken stoppingToken)
    {
        var appKey = requestData.AppKey.ToAppKey();
        if (appKey.IsUnknown())
        {
            errors.Add("App Key is required.");
        }
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