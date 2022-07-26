namespace HubWebApp.ApiControllers;
partial class EdmModelBuilder
{
    partial void init()
    {
        UserQuery.EntityType.HasKey(u => u.UserID);
        SessionQuery.EntityType.HasKey(s => s.SessionID);
        RequestQuery.EntityType.HasKey(r => r.RequestID);
    }
}