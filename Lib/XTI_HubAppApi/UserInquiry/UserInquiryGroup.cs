using XTI_Hub;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.UserInquiry
{
    public sealed class UserInquiryGroup : AppApiGroupWrapper
    {
        public UserInquiryGroup(AppApiGroup source, UserInquiryGroupFactory factory)
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            GetUser = source.AddAction(actions.Action(nameof(GetUser), factory.CreateGetUser));
            GetUserByUserName = source.AddAction(actions.Action(nameof(GetUserByUserName), factory.CreateGetUserByUserName));
            GetCurrentUser = source.AddAction(actions.Action(nameof(GetCurrentUser), factory.CreateGetCurrentUser));
            RedirectToAppUser = source.AddAction(actions.Action(nameof(RedirectToAppUser), factory.CreateRedirectToAppUser));
        }

        public AppApiAction<int, AppUserModel> GetUser { get; }
        public AppApiAction<string, AppUserModel> GetUserByUserName { get; }
        public AppApiAction<EmptyRequest, AppUserModel> GetCurrentUser { get; }
        public AppApiAction<RedirectToAppUserRequest, WebRedirectResult> RedirectToAppUser { get; }
    }
}
