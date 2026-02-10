using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class InstallCommandLog
{
    private readonly IHubService hubService;

    public InstallCommandLog(IHubService hubService)
    {
        this.hubService = hubService;
    }

    public async Task<AppInstallCommandDetailModel> WriteLog(AppInstallCommandDetailModel installCommandDetail, CancellationToken ct)
    {
        var timeout = DateTime.UtcNow.AddMinutes(5);
        var previousStep = installCommandDetail.Steps.LastOrDefault() ?? new();
        Console.WriteLine($"{installCommandDetail.Command.TimeAdded:HH:mm:ss} Install {installCommandDetail.App.AppKey.Format()} {installCommandDetail.Version.VersionKey.DisplayText} Requested");
        while (!installCommandDetail.Command.HasStarted() && DateTime.UtcNow < timeout)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), ct);
            installCommandDetail = await hubService.GetInstallCommandDetail(installCommandDetail.Command.ID, ct);
        }
        if (installCommandDetail.Command.HasStarted())
        {
            Console.WriteLine($"{installCommandDetail.Command.TimeStarted:HH:mm:ss} Install {installCommandDetail.App.AppKey.Format()} {installCommandDetail.Version.VersionKey.DisplayText} Started");
        }
        while (!installCommandDetail.Command.HasEnded() && DateTime.UtcNow < timeout)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), ct);
            installCommandDetail = await hubService.GetInstallCommandDetail(installCommandDetail.Command.ID, ct);
            if (installCommandDetail.Steps.Any())
            {
                var steps = installCommandDetail.Steps.ToList();
                var index = steps.FindIndex(s => s.ID == previousStep.ID);
                if(index > -1)
                {
                    if (!previousStep.HasEnded() && steps[index].HasEnded())
                    {
                        Console.WriteLine($"{steps[index].TimeEnded:HH:mm:ss} {steps[index].Activity} Ended {steps[index].ErrorMessage}".Trim());
                    }
                }
                foreach(var step in steps.Skip(index + 1).ToArray())
                {
                    if (step.HasStarted())
                    {
                        Console.WriteLine($"{step.TimeStarted:HH:mm:ss} {step.Activity} Started");
                    }
                    if (step.HasEnded())
                    {
                        var status = step.HasError() ? "Failed" : "Ended";
                        Console.WriteLine($"{step.TimeEnded:HH:mm:ss} {step.Activity} {status} {step.ErrorMessage}".Trim());
                    }
                }
            }
            previousStep = installCommandDetail.Steps.LastOrDefault() ?? new();
        }
        if (installCommandDetail.Command.HasEnded())
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} Install {installCommandDetail.App.AppKey.Format()} {installCommandDetail.Version.VersionKey.DisplayText} Complete");
        }
        return installCommandDetail;
    }
}
