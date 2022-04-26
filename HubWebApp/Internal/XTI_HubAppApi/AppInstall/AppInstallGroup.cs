using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_Hub;
using XTI_Hub.Abstractions;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppInstall;

public sealed class AppInstallGroup : AppApiGroupWrapper
{
    public AppInstallGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        RegisterApp = source.AddAction
        (
            actions.Action(nameof(RegisterApp), () => sp.GetRequiredService<RegisterAppAction>())
        );
        AddOrUpdateApps = source.AddAction
        (
            actions.Action
            (
                nameof(AddOrUpdateApps),
                () => sp.GetRequiredService<AddOrUpdateAppsValidation>(), 
                () => sp.GetRequiredService<AddOrUpdateAppsAction>()
            )
        );
        AddOrUpdateVersions = source.AddAction
        (
            actions.Action(nameof(AddOrUpdateVersions), () => sp.GetRequiredService<AddOrUpdateVersionsAction>())
        );
        GetVersion = source.AddAction
        (
            actions.Action(nameof(GetVersion), () => sp.GetRequiredService<GetVersionAction>())
        );
        GetVersions = source.AddAction
        (
            actions.Action(nameof(GetVersions), () => sp.GetRequiredService<GetVersionsAction>())
        );
        AddSystemUser = source.AddAction
        (
            actions.Action
            (
                nameof(AddSystemUser),
                () => sp.GetRequiredService<AddSystemUserValidation>(),
                () => sp.GetRequiredService<AddSystemUserAction>()
            )
        );
        AddInstallationUser = source.AddAction
        (
            actions.Action
            (
                nameof(AddInstallationUser),
                () => sp.GetRequiredService<AddInstallationUserAction>()
            )
        );
        NewInstallation = source.AddAction
        (
            actions.Action
            (
                nameof(NewInstallation),
                () => sp.GetRequiredService<NewInstallationValidation>(),
                () => sp.GetRequiredService<NewInstallationAction>()
            )
        );
        BeginCurrentInstallation = source.AddAction
        (
            actions.Action(nameof(BeginCurrentInstallation), () => sp.GetRequiredService<BeginCurrentInstallationAction>())
        );
        BeginVersionInstallation = source.AddAction
        (
            actions.Action(nameof(BeginVersionInstallation), () => sp.GetRequiredService<BeginVersionInstallationAction>())
        );
        Installed = source.AddAction
        (
            actions.Action(nameof(Installed), () => sp.GetRequiredService<InstalledAction>())
        );
    }

    public AppApiAction<RegisterAppRequest, AppWithModKeyModel> RegisterApp { get; }
    public AppApiAction<GetVersionRequest, XtiVersionModel> GetVersion { get; }
    public AppApiAction<GetVersionsRequest, XtiVersionModel[]> GetVersions { get; }
    public AppApiAction<AddOrUpdateAppsRequest, AppModel[]> AddOrUpdateApps { get; }
    public AppApiAction<AddOrUpdateVersionsRequest, EmptyActionResult> AddOrUpdateVersions { get; }
    public AppApiAction<AddSystemUserRequest, AppUserModel> AddSystemUser { get; }
    public AppApiAction<AddInstallationUserRequest, AppUserModel> AddInstallationUser { get; }
    public AppApiAction<NewInstallationRequest, NewInstallationResult> NewInstallation { get; }
    public AppApiAction<BeginInstallationRequest, int> BeginCurrentInstallation { get; }
    public AppApiAction<BeginInstallationRequest, int> BeginVersionInstallation { get; }
    public AppApiAction<InstalledRequest, EmptyActionResult> Installed { get; }
}