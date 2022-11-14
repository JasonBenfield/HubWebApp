using XTI_Forms;

namespace XTI_Hub.Abstractions;

public sealed class EditCurrentUserForm : Form
{
    public EditCurrentUserForm() : base(nameof(EditCurrentUserForm))
    {
        PersonName = AddTextInput(nameof(PersonName));
        Email = AddTextInput(nameof(Email));
    }

    public InputField<string> PersonName { get; }
    public InputField<string> Email { get; }
}