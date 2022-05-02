namespace XTI_Hub;

public sealed class GuidGeneratedStorageKey : IGeneratedStorageKey
{
    public string Value() => Guid.NewGuid().ToString("N");
}
