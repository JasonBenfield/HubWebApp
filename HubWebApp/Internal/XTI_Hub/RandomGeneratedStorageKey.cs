namespace XTI_Hub;

public sealed class RandomGeneratedStorageKey : IGeneratedStorageKey
{
    private readonly long minValue;
    private readonly long maxValue;

    public RandomGeneratedStorageKey(int numberOfDigits)
    {
        minValue = (long)Math.Pow(10, numberOfDigits - 1);
        maxValue = (long)Math.Pow(10, numberOfDigits) - 1;
    }

    public string Value() => new Random((int)DateTimeOffset.Now.Ticks).NextInt64(minValue, maxValue).ToString();
}
