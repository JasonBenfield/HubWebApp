using XTI_Forms;

namespace XTI_Hub.Abstractions;

public sealed class ChangeCurrentUserPasswordForm : Form
{
    public ChangeCurrentUserPasswordForm() : base(nameof(ChangeCurrentUserPasswordForm))
    {
        Password = AddTextInput(nameof(Password));
        Password.Protect();
        Password.MustNotBeNull();
        Password.MustNotBeWhiteSpace();
        Confirm = AddTextInput(nameof(Confirm));
        Confirm.Protect();
        Confirm.MustNotBeNull();
        Confirm.MustBeEqualToField(Password);
    }

    public InputField<string> Password { get; }
    public InputField<string> Confirm { get; }
}