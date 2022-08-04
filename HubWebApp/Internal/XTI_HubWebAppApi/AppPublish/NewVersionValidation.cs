using XTI_Core;

namespace XTI_HubWebAppApi.AppPublish;

internal sealed class NewVersionValidation : AppActionValidation<NewVersionRequest>
{
    public Task Validate(ErrorList errors, NewVersionRequest model, CancellationToken stoppingToken)
    {
        if (model.VersionType.Equals(AppVersionType.Values.NotSet))
        {
            errors.Add("Version type is required");
        }
        return Task.CompletedTask;
    }
}