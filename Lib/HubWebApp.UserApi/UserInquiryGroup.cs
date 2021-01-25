using XTI_App;
using XTI_App.Api;

namespace HubWebApp.UserApi
{
    public sealed class UserInquiryGroup : AppApiGroupWrapper
    {
        public UserInquiryGroup(AppApiGroup source, UserInquiryGroupFactory factory)
            : base(source)
        {
            var actions = new AppApiActionFactory(source);
            GetUser = source.AddAction(actions.Action(nameof(GetUser), factory.CreateGetUser));
        }

        public AppApiAction<int, AppUserModel> GetUser { get; }
    }
}
