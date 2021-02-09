using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace HubWebAppApi.Users
{
    public sealed class UserInquiryGroup : AppApiGroupWrapper
    {
        public UserInquiryGroup(AppApiGroup source, UserInquiryGroupFactory factory)
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            GetUser = source.AddAction(actions.Action(nameof(GetUser), factory.CreateGetUser));
            RedirectToAppUser = source.AddAction(actions.Action(nameof(RedirectToAppUser), factory.CreateRedirectToAppUser));
        }

        public AppApiAction<int, AppUserModel> GetUser { get; }
        public AppApiAction<RedirectToAppUserRequest, WebRedirectResult> RedirectToAppUser { get; }
    }
}
