using HubWebApp.Core;
using XTI_App.Api;

namespace HubWebApp.UserAdminApi
{
    public sealed class UserAdminGroup : AppApiGroup
    {
        public UserAdminGroup(AppApi api, IAppApiUser user, UserAdminGroupFactory factory)
            : base
            (
                  api,
                  new NameFromGroupClassName(nameof(UserAdminGroup)).Value,
                  false,
                  api.Access.WithAllowed(HubRoles.Instance.Admin),
                  user
            )
        {
            AddUser = AddAction
            (
                nameof(AddUser),
                () => new AddUserValidation(),
                () => factory.CreateAddUserAction()
            );
        }

        public AppApiAction<AddUserModel, int> AddUser { get; }
    }
}
