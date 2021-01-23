using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebApp.UserAdminApi
{
    public sealed class UserAdminGroup : AppApiGroupWrapper
    {
        public UserAdminGroup(AppApiGroup source, UserAdminActionFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            Index = source.AddAction(actions.DefaultView());
            AddUser = source.AddAction
            (
                actions.Action
                (
                    nameof(AddUser),
                    () => new AddUserValidation(),
                    () => factory.CreateAddUserAction()
                )
            );
        }

        public AppApiAction<EmptyRequest, WebViewResult> Index { get; }

        public AppApiAction<AddUserModel, int> AddUser { get; }
    }
}
