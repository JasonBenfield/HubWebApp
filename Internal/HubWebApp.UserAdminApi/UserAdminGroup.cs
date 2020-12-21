using HubWebApp.Core;
using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.UserAdminApi
{
    public sealed class UserAdminGroup : AppApiGroup
    {
        public UserAdminGroup(AppApi api, ResourceAccess access, IAppApiUser user, UserAdminActionFactory factory)
            : base
            (
                  api,
                  new NameFromGroupClassName(nameof(UserAdminGroup)).Value,
                  ModifierCategoryName.Default,
                  access,
                  user,
                  (n, a, u) => new WebAppApiActionCollection(n, a, u)
            )
        {
            var actions = Actions<WebAppApiActionCollection>();
            Index = actions.AddDefaultView();
            AddUser = actions.AddAction
            (
                nameof(AddUser),
                () => new AddUserValidation(),
                () => factory.CreateAddUserAction()
            );
        }

        public AppApiAction<EmptyRequest, AppActionViewResult> Index { get; }

        public AppApiAction<AddUserModel, int> AddUser { get; }
    }
}
