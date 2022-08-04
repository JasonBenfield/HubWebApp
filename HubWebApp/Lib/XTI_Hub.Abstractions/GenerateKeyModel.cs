namespace XTI_Hub.Abstractions;

public sealed record GenerateKeyModel(GeneratedKeyType KeyType, string Value)
{
    public static GenerateKeyModel Fixed(string value) =>
        new GenerateKeyModel(GeneratedKeyType.Values.Fixed, value);

    public static GenerateKeyModel Guid() =>
        new GenerateKeyModel(GeneratedKeyType.Values.Guid, "");

    public static GenerateKeyModel SixDigit() => 
        new GenerateKeyModel(GeneratedKeyType.Values.SixDigit, "");

    public static GenerateKeyModel TenDigit() =>
        new GenerateKeyModel(GeneratedKeyType.Values.TenDigit, "");

    public GenerateKeyModel()
        :this(GeneratedKeyType.Values.Guid, "")
    {
    }
}