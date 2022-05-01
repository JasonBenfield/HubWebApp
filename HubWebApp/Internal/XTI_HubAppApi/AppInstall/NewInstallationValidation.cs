using XTI_Core;

namespace XTI_HubAppApi.AppInstall;

internal sealed class NewInstallationValidation : AppActionValidation<NewInstallationRequest>
{
    public Task Validate(ErrorList errors, NewInstallationRequest model)
    {
        if (model.VersionName.Equals(AppVersionName.None) || model.VersionName.Equals(AppVersionName.Unknown))
        {
            errors.Add(AppErrors.VersionNameIsRequired);
        }
        if (string.IsNullOrWhiteSpace(model.QualifiedMachineName))
        {
            errors.Add(AppErrors.MachineNameIsRequired);
        }
        return Task.CompletedTask;
    }
}
