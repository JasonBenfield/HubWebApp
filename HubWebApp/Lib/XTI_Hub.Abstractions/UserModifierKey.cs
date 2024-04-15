namespace XTI_Hub.Abstractions;

public sealed class UserModifierKey
{
    public UserModifierKey()
        : this(0, 0)
    {
    }

    public UserModifierKey(int userID, int modifierID)
    {
        UserID = userID;
        ModifierID = modifierID;
    }

    public int UserID { get; set; }
    public int ModifierID { get; set; }
}
