using XTI_App.Api;

namespace HubWebApp.UserAdminApi
{
    public sealed class UserAdminGroup : AppApiGroup
    {
        public UserAdminGroup(AppApi api, AppApiUser user, IUserAdminFactory factory)
            : base
            (
                  api,
                  new NameFromGroupClassName(nameof(UserAdminGroup)).Value,
                  ResourceAccess.AllowAuthenticated(),
                  user
            )
        {
            AddUser = AddAction
            (
                nameof(AddUser),
                u => new AddUserValidation(),
                u => factory.CreateAddUserAction()
            );
        }

        public AppApiAction<AddUserModel, int> AddUser { get; }
    }
}
