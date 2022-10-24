using XTI_Forms;

namespace XTI_Hub.Abstractions;

public sealed class ChangePasswordForm : Form
{
    public ChangePasswordForm() : base(nameof(ChangePasswordForm))
    {
        UserID = AddInt32Hidden(nameof(UserID));
        UserID.MustNotBeNull();
        Password = AddTextInput(nameof(Password));
        Password.Protect();
        Password.MustNotBeNull();
        Password.MustNotBeWhiteSpace();
        Confirm = AddTextInput(nameof(Confirm));
        Confirm.Protect();
        Confirm.MustNotBeNull();
        Confirm.MustBeEqualToField(Password);
    }

    public HiddenField<int?> UserID { get; }
    public InputField<string> Password { get; }
    public InputField<string> Confirm { get; }
}