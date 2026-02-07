using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Installation;

public sealed class RequestedInstallation
{
    private readonly IHubService hubService;
    private readonly AppInstallCommandDetailModel requestedInstallationDetail;
    private bool isCurrent;

    public RequestedInstallation(IHubService hubService, AppInstallCommandDetailModel requestedInstallationDetail)
    {
        this.hubService = hubService;
        this.requestedInstallationDetail = requestedInstallationDetail;
    }

    public bool IsCurrent { get => isCurrent; }

    public AppKey AppKey { get => requestedInstallationDetail.App.AppKey; }

    public AppVersionKey VersionKey
    {
        get => isCurrent ?
            AppVersionKey.Current :
            requestedInstallationDetail.Version.VersionKey;
    }

    public string SiteName { get => requestedInstallationDetail.InstallConfiguration.Template.SiteName; }

    public void SetIsCurrent(bool isCurrent) => this.isCurrent = isCurrent;

    public async Task RunStep(string activity, Func<Task> action, CancellationToken ct)
    {
        var step = await hubService.BeginCommandStep(requestedInstallationDetail.Command.ID, activity, ct);
        try
        {
            await action();
            await hubService.CommandStepEnded(step.ID, "", ct);
        }
        catch (Exception ex)
        {
            await hubService.CommandStepEnded(step.ID, ex.ToString(), ct);
            throw new RequestedInstallationStepException(step);
        }
    }

    public async Task<T> RunStep<T>(string activity, Func<Task<T>> action, CancellationToken ct)
    {
        T result;
        var step = await hubService.BeginCommandStep(requestedInstallationDetail.Command.ID, activity, ct);
        try
        {
            result = await action();
            await hubService.CommandStepEnded(step.ID, "", ct);
        }
        catch (Exception ex)
        {
            await hubService.CommandStepEnded(step.ID, ex.ToString(), ct);
            throw new RequestedInstallationStepException(step);
        }
        return result;
    }
}
