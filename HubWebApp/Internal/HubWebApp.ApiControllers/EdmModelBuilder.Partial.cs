namespace HubWebApp.ApiControllers;
partial class EdmModelBuilder
{
    partial void init()
    {
        UserQuery.EntityType.HasKey(u => u.UserID);
        SessionQuery.EntityType.HasKey(s => s.SessionID);
        AppRequestQuery.EntityType.HasKey(r => r.RequestID);
        LogEntryQuery.EntityType.HasKey(e => e.EventID);
        InstallationQuery.EntityType.HasKey(e => e.InstallationID);
        UserRoleQuery.EntityType.HasKey(e => e.UserRoleID);
    }
}