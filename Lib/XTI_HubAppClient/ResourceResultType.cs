// Generated Code
namespace XTI_HubAppClient;
public sealed class ResourceResultType : NumericValue
{
    public sealed class ResourceResultTypes : NumericValues<ResourceResultType>
    {
        internal ResourceResultTypes()
        {
            None = Add(new ResourceResultType(0, "None"));
            View = Add(new ResourceResultType(1, "View"));
            PartialView = Add(new ResourceResultType(2, "PartialView"));
            Redirect = Add(new ResourceResultType(3, "Redirect"));
            Json = Add(new ResourceResultType(4, "Json"));
        }

        public ResourceResultType None { get; }

        public ResourceResultType View { get; }

        public ResourceResultType PartialView { get; }

        public ResourceResultType Redirect { get; }

        public ResourceResultType Json { get; }
    }

    public static readonly ResourceResultTypes Values = new ResourceResultTypes();
    private ResourceResultType(int value, string displayText) : base(value, displayText)
    {
    }
}