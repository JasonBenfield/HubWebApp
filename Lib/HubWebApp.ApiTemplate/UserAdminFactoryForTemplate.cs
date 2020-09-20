using HubWebApp.UserAdminApi;
using XTI_WebApp.Api;

namespace HubWebApp.ApiTemplate
{
    public sealed class UserAdminFactoryForTemplate : IUserAdminFactory
    {
        public AppAction<AddUserModel, int> CreateAddUserAction() => new EmptyAppAction<AddUserModel, int>();
    }
}
