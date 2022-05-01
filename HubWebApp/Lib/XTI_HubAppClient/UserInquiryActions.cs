// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserInquiryActions
{
    internal UserInquiryActions(AppClientUrl appClientUrl)
    {
        RedirectToAppUser = new AppClientGetAction<RedirectToAppUserRequest>(appClientUrl, "RedirectToAppUser");
    }

    public AppClientGetAction<RedirectToAppUserRequest> RedirectToAppUser { get; }
}