namespace XTI_Hub;

public sealed class FixedGeneratedKey : IGeneratedKey
{
    private readonly string value;

    public FixedGeneratedKey(string value)
    {
        this.value = value;
    }

    public string Value() => value;
}
