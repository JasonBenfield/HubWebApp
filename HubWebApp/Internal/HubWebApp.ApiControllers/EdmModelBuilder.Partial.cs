namespace HubWebApp.ApiControllers;
partial class EdmModelBuilder
{
    partial void init()
    {
        UserQuery.EntityType.HasKey(u => u.UserID);
    }
}