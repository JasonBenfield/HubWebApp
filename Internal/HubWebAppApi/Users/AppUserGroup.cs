using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebAppApi.Users
{
    public sealed class AppUserGroup : AppApiGroupWrapper
    {
        public AppUserGroup(AppApiGroup source, AppUserGroupFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.View(nameof(Index), factory.CreateIndex));
            GetUserRoles = source.AddAction(actions.Action(nameof(GetUserRoles), factory.CreateGetUserRoles));
            GetUserRoleAccess = source.AddAction(actions.Action(nameof(GetUserRoleAccess), factory.CreateGetUserRoleAccess));
        }
        public AppApiAction<int, WebViewResult> Index { get; }
        public AppApiAction<int, AppUserRoleModel[]> GetUserRoles { get; }
        public AppApiAction<int, UserRoleAccessModel> GetUserRoleAccess { get; }
    }
}
