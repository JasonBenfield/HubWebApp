// Generated Code
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace HubWebApp.ApiControllers;
public sealed partial class EdmModelBuilder
{
    private readonly ODataConventionModelBuilder odataBuilder = new();
    public EdmModelBuilder()
    {
        UserQuery = odataBuilder.EntitySet<ExpandedUser>("UserQuery");
        init();
    }

    partial void init();
    public EntitySetConfiguration<ExpandedUser> UserQuery { get; }

    public IEdmModel GetEdmModel() => odataBuilder.GetEdmModel();
}