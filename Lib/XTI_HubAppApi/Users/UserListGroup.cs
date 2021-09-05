using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Users
{
    public sealed class UserListGroup : AppApiGroupWrapper
    {
        public UserListGroup(AppApiGroup source, UserListGroupFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.Action(nameof(Index), factory.CreateIndex));
            GetUsers = source.AddAction(actions.Action(nameof(GetUsers), factory.CreateGetUsers));
            AddUser = source.AddAction
            (
                actions.Action
                (
                    nameof(AddUser),
                    Access.WithAllowed(HubInfo.Roles.AddUser),
                    factory.CreateAddUserValidation,
                    factory.CreateAddUser
                )
            );
        }
        public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
        public AppApiAction<EmptyRequest, AppUserModel[]> GetUsers { get; }
        public AppApiAction<AddUserModel, int> AddUser { get; }
    }
}
