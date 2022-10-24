using XTI_Forms;

namespace XTI_Hub.Abstractions;

public sealed class AddUserForm : Form
{
    public AddUserForm() : base(nameof(AddUserForm))
    {
        UserName = AddTextInput(nameof(UserName));
        UserName.MustNotBeNull();
        UserName.MustNotBeWhiteSpace();
        Password = AddTextInput(nameof(Password));
        Password.Protect();
        Password.MustNotBeNull();
        Password.MustNotBeWhiteSpace();
        Confirm = AddTextInput(nameof(Confirm));
        Confirm.Protect();
        Confirm.MustNotBeNull();
        Confirm.MustBeEqualToField(Password);
        PersonName = AddTextInput(nameof(PersonName));
        PersonName.Caption = "Name";
        Email = AddTextInput(nameof(Email));
    }

    public InputField<string> UserName { get; }
    public InputField<string> PersonName { get; }
    public InputField<string> Email { get; }
    public InputField<string> Password { get; }
    public InputField<string> Confirm { get; }
}