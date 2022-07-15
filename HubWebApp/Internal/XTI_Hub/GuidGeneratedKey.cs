namespace XTI_Hub;

public sealed class GuidGeneratedKey : IGeneratedKey
{
    public string Value() => Guid.NewGuid().ToString("N");
}
