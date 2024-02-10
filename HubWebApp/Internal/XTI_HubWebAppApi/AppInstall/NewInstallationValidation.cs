using XTI_Core;

namespace XTI_HubWebAppApi.AppInstall;

internal sealed class NewInstallationValidation : AppActionValidation<NewInstallationRequest>
{
    public Task Validate(ErrorList errors, NewInstallationRequest addRequest, CancellationToken stoppingToken)
    {
        var versionName = addRequest.ToAppVersionName();
        if (versionName.Equals(AppVersionName.None) || versionName.Equals(AppVersionName.Unknown))
        {
            errors.Add(AppErrors.VersionNameIsRequired);
        }
        if (string.IsNullOrWhiteSpace(addRequest.QualifiedMachineName))
        {
            errors.Add(AppErrors.MachineNameIsRequired);
        }
        return Task.CompletedTask;
    }
}
