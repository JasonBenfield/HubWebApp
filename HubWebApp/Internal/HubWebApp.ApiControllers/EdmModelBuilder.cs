// Generated Code
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace HubWebApp.ApiControllers;
public sealed partial class EdmModelBuilder
{
    private readonly ODataConventionModelBuilder odataBuilder = new();
    public EdmModelBuilder()
    {
        InstallationQuery = odataBuilder.EntitySet<ExpandedInstallation>("InstallationQuery");
        AppRequestQuery = odataBuilder.EntitySet<ExpandedRequest>("AppRequestQuery");
        LogEntryQuery = odataBuilder.EntitySet<ExpandedLogEntry>("LogEntryQuery");
        SessionQuery = odataBuilder.EntitySet<ExpandedSession>("SessionQuery");
        UserQuery = odataBuilder.EntitySet<ExpandedUser>("UserQuery");
        UserRoleQuery = odataBuilder.EntitySet<ExpandedUserRole>("UserRoleQuery");
        init();
    }

    partial void init();
    public EntitySetConfiguration<ExpandedInstallation> InstallationQuery { get; }
    public EntitySetConfiguration<ExpandedRequest> AppRequestQuery { get; }
    public EntitySetConfiguration<ExpandedLogEntry> LogEntryQuery { get; }
    public EntitySetConfiguration<ExpandedSession> SessionQuery { get; }
    public EntitySetConfiguration<ExpandedUser> UserQuery { get; }
    public EntitySetConfiguration<ExpandedUserRole> UserRoleQuery { get; }

    public IEdmModel GetEdmModel() => odataBuilder.GetEdmModel();
}