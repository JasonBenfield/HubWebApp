using XTI_App.Api;

namespace HubWebApp.UserAdminApi
{
    public interface IUserAdminFactory
    {
        AppAction<AddUserModel, int> CreateAddUserAction();
    }
}
