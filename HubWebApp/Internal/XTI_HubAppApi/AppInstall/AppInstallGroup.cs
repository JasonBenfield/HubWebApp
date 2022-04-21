using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppInstall;

public sealed class AppInstallGroup : AppApiGroupWrapper
{
    public AppInstallGroup(AppApiGroup source, IServiceProvider services)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        RegisterApp = source.AddAction
        (
            actions.Action(nameof(RegisterApp), () => services.GetRequiredService<RegisterAppAction>())
        );
        AddOrUpdateVersions = source.AddAction
        (
            actions.Action(nameof(AddOrUpdateVersions), () => services.GetRequiredService<AddOrUpdateVersions>())
        );
        GetVersion = source.AddAction
        (
            actions.Action(nameof(GetVersion), () => services.GetRequiredService<GetVersionAction>())
        );
        GetVersions = source.AddAction
        (
            actions.Action(nameof(GetVersions), () => services.GetRequiredService<GetVersionsAction>())
        );
        AddSystemUser = source.AddAction
        (
            actions.Action
            (
                nameof(AddSystemUser),
                () => services.GetRequiredService<AddSystemUserValidation>(),
                () => services.GetRequiredService<AddSystemUserAction>()
            )
        );
        AddInstallationUser = source.AddAction
        (
            actions.Action
            (
                nameof(AddInstallationUser),
                () => services.GetRequiredService<AddInstallationUserAction>()
            )
        );
        NewInstallation = source.AddAction
        (
            actions.Action(nameof(NewInstallation), () => services.GetRequiredService<NewInstallationAction>())
        );
        BeginCurrentInstallation = source.AddAction
        (
            actions.Action(nameof(BeginCurrentInstallation), () => services.GetRequiredService<BeginCurrentInstallationAction>())
        );
        BeginVersionInstallation = source.AddAction
        (
            actions.Action(nameof(BeginVersionInstallation), () => services.GetRequiredService<BeginVersionInstallationAction>())
        );
        Installed = source.AddAction
        (
            actions.Action(nameof(Installed), () => services.GetRequiredService<InstalledAction>())
        );
    }

    public AppApiAction<RegisterAppRequest, AppWithModKeyModel> RegisterApp { get; }
    public AppApiAction<GetVersionRequest, XtiVersionModel> GetVersion { get; }
    public AppApiAction<GetVersionsRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiAction<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions { get; }
    public AppApiAction<AddSystemUserRequest, AppUserModel> AddSystemUser { get; }
    public AppApiAction<AddInstallationUserRequest, AppUserModel> AddInstallationUser { get; }
    public AppApiAction<NewInstallationRequest, NewInstallationResult> NewInstallation { get; }
    public AppApiAction<BeginInstallationRequest, int> BeginCurrentInstallation { get; }
    public AppApiAction<BeginInstallationRequest, int> BeginVersionInstallation { get; }
    public AppApiAction<InstalledRequest, EmptyActionResult> Installed { get; }
}