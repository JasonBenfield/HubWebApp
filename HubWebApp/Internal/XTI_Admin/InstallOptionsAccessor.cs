using XTI_App.Abstractions;

namespace XTI_Admin;

public sealed record InstallOptions(InstallationOptions[] Installations, int Sequence = 999);

public sealed record InstallationOptions(string MachineName = "", string Domain = "", string SiteName = "");

public sealed class InstallOptionsAccessor
{
    private readonly AdminOptions options;
    private readonly SlnFolder slnFolder;

    public InstallOptionsAccessor(AdminOptions options, SlnFolder slnFolder)
    {
        this.options = options;
        this.slnFolder = slnFolder;
    }

    public InstallationOptions[] Installations(AppKey appKey)
    {
        var installationOptions = new List<InstallationOptions>();
        if (!appKey.Type.Equals(AppType.Values.Package))
        {
            var slnAppFolder = slnFolder.Folder(appKey);
            if
            (
                string.IsNullOrWhiteSpace(options.DestinationMachine) &&
                string.IsNullOrWhiteSpace(options.Domain) &&
                string.IsNullOrWhiteSpace(options.SiteName) &&
                slnAppFolder != null
            )
            {
                installationOptions.AddRange(slnAppFolder.InstallOptions.Installations);
            }
            else
            {
                installationOptions.Add
                (
                    new InstallationOptions
                    (
                        options.DestinationMachine,
                        appKey.Type.Equals(AppType.Values.WebApp) ? options.Domain : "",
                        appKey.Type.Equals(AppType.Values.WebApp) ? options.SiteName : ""
                    )
                );
            }
        }
        return installationOptions.ToArray();
    }
}
