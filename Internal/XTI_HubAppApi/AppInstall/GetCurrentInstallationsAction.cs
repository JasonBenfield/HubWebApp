using XTI_App.Api;
using XTI_Hub;

namespace XTI_HubAppApi.AppInstall;

public sealed class GetCurrentInstallationsAction : AppAction<GetCurrentInstallationsRequest, InstallationModel>
{
    private readonly AppFactory appFactory;

    public GetCurrentInstallationsAction(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task<InstallationModel> Execute(GetCurrentInstallationsRequest model)
    {
        return new InstallationModel();
    }
}