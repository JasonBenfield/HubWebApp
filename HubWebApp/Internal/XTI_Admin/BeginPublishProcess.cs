using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class BeginPublishProcess
{
    private readonly Scopes scopes;

    public BeginPublishProcess(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task<XtiVersionModel> Run()
    {
        Console.WriteLine("Begin Publishing");
        var versionKey = new VersionKeyFromCurrentBranch(scopes).Value();
        var hubAdmin = scopes.Production().GetRequiredService<IHubAdministration>();
        var version = await hubAdmin.BeginPublish(scopes.GetRequiredService<AppVersionNameAccessor>().Value, versionKey);
        return version;
    }
}