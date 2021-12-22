using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.VersionInquiry;

public sealed class GetVersionAction : AppAction<string, AppVersionModel>
{
    private readonly AppFromPath appFromPath;

    public GetVersionAction(AppFromPath appFromPath)
    {
        this.appFromPath = appFromPath;
    }

    public async Task<AppVersionModel> Execute(string versionKey)
    {
        var app = await appFromPath.Value();
        var version = await app.Version(AppVersionKey.Parse(versionKey));
        return version.ToModel();
    }
}