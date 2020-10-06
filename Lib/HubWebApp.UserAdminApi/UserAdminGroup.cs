using HubWebApp.Core;
using XTI_App.Api;

namespace HubWebApp.UserAdminApi
{
    public sealed class UserAdminGroup : AppApiGroup
    {
        public UserAdminGroup(AppApi api, IAppApiUser user, IUserAdminFactory factory)
            : base
            (
                  api,
                  new NameFromGroupClassName(nameof(UserAdminGroup)).Value,
                  false,
                  ResourceAccess.AllowAuthenticated().WithAllowed(HubRoles.Instance.Admin),
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
