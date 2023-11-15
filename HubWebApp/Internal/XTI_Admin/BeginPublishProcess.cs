using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Admin;

public sealed class BeginPublishProcess
{
    private readonly VersionKeyFromCurrentBranch versionKeyFromCurrentBranch;
    private readonly ProductionHubAdmin productionHubAdmin;
    private readonly AppVersionNameAccessor versionNameAccessor;

    public BeginPublishProcess(VersionKeyFromCurrentBranch versionKeyFromCurrentBranch, ProductionHubAdmin productionHubAdmin, AppVersionNameAccessor versionNameAccessor)
    {
        this.versionKeyFromCurrentBranch = versionKeyFromCurrentBranch;
        this.productionHubAdmin = productionHubAdmin;
        this.versionNameAccessor = versionNameAccessor;
    }

    public async Task<XtiVersionModel> Run()
    {
        Console.WriteLine("Begin Publishing");
        var versionKey = versionKeyFromCurrentBranch.Value();
        var version = await productionHubAdmin.Value.BeginPublish(versionNameAccessor.Value, versionKey);
        return version;
    }
}