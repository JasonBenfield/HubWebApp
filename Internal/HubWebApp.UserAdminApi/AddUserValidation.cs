using System.Threading.Tasks;
using XTI_App.Api;
using XTI_Core;

namespace HubWebApp.UserAdminApi
{
    public sealed class AddUserValidation : AppActionValidation<AddUserModel>
    {
        public Task Validate(ErrorList errors, AddUserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                errors.Add(UserAdminErrors.UserNameIsRequired, "User Name", nameof(model.UserName));
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                errors.Add(UserAdminErrors.PasswordIsRequired, "Password", nameof(model.Password));
            }
            return Task.CompletedTask;
        }
    }
}
