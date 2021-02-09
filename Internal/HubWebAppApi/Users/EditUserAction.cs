using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Api;
using XTI_Core;

namespace HubWebAppApi.Users
{
    public sealed class EditUserAction : AppAction<EditUserForm, EmptyActionResult>
    {
        private readonly AppFactory factory;

        public EditUserAction(AppFactory factory)
        {
            this.factory = factory;
        }

        public async Task<EmptyActionResult> Execute(EditUserForm model)
        {
            var userID = model.UserID.Value();
            var user = await factory.Users().User(userID.Value);
            var name = new PersonName(model.PersonName.Value());
            if (string.IsNullOrWhiteSpace(name))
            {
                name = new PersonName(user.UserName());
            }
            var email = new EmailAddress(model.Email.Value());
            await user.Edit(name, email);
            return new EmptyActionResult();
        }
    }
}
