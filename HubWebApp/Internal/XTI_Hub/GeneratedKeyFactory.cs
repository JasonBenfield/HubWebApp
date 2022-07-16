using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class GeneratedKeyFactory
{
    public IGeneratedKey Create(GenerateKeyModel generateKeyModel)
    {
        IGeneratedKey generatedKey;
        if (generateKeyModel.KeyType.Equals(GeneratedKeyType.Values.Guid))
        {
            generatedKey = new GuidGeneratedKey();
        }
        else if (generateKeyModel.KeyType.Equals(GeneratedKeyType.Values.Fixed))
        {
            generatedKey = new FixedGeneratedKey(generateKeyModel.Value);
        }
        else
        {
            var numberOfDigits = generateKeyModel.KeyType.Value;
            generatedKey = new RandomGeneratedKey(numberOfDigits);
        }
        return generatedKey;
    }
}
