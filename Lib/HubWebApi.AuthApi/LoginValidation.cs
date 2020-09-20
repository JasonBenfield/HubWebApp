using System.Threading.Tasks;
using XTI_WebApp.Api;

namespace HubWebApp.AuthApi
{
    public sealed class LoginValidation : AppActionValidation<LoginModel>
    {
        public Task Validate(ErrorList errors, LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName))
            {
                errors.Add(ValidationErrors.UserNameIsRequired, nameof(model.UserName));
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                errors.Add(ValidationErrors.PasswordIsRequired, nameof(model.Password));
            }
            return Task.CompletedTask;
        }
    }
}
