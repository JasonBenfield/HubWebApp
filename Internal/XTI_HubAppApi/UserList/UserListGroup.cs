using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.UserList;

public sealed class UserListGroup : AppApiGroupWrapper
{
    public UserListGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        Index = source.AddAction(actions.Action(nameof(Index), () => sp.GetRequiredService<IndexAction>()));
        GetUsers = source.AddAction(actions.Action(nameof(GetUsers), () => sp.GetRequiredService<GetUsersAction>()));
        GetSystemUsers = source.AddAction(actions.Action(nameof(GetSystemUsers), () => sp.GetRequiredService<GetSystemUsersAction>()));
        AddUser = source.AddAction
        (
            actions.Action
            (
                nameof(AddUser),
                Access.WithAllowed(HubInfo.Roles.AddUser),
                () => sp.GetRequiredService<AddUserValidation>(),
                () => sp.GetRequiredService<AddUserAction>()
            )
        );
    }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<EmptyRequest, AppUserModel[]> GetUsers { get; }
    public AppApiAction<AppKey, AppUserModel[]> GetSystemUsers { get; }
    public AppApiAction<AddUserModel, int> AddUser { get; }
}