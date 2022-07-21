using XTI_Forms;

namespace XTI_HubWebAppApi.Auth;

public sealed class VerifyLoginForm : Form
{
    public VerifyLoginForm() 
        : base(nameof(VerifyLoginForm))
    {
        UserName = AddTextInput(nameof(UserName));
        UserName.MustNotBeNull();
        UserName.MustNotBeWhiteSpace();
        UserName.MaxLength = 100;
        Password = AddTextInput(nameof(Password));
        Password.Protect();
        Password.MustNotBeNull();
        Password.MustNotBeWhiteSpace();
        Password.MaxLength = 100;
    }

    public InputField<string> UserName { get; }
    public InputField<string> Password { get; }
}