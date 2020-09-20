using System;
using System.Collections.Generic;
using System.Text;
using XTI_WebApp.Api;

namespace HubWebApp.UserAdminApi
{
    public interface IUserAdminFactory
    {
        AppAction<AddUserModel, int> CreateAddUserAction();
    }
}
