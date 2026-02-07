namespace XTI_Hub.Abstractions;

public sealed record AppCommandStepModel
(
    int ID,
    string Activity,
    DateTimeOffset TimeStarted,
    DateTimeOffset TimeEnded,
    string ErrorMessage
)
{
    public AppCommandStepModel()
        : this(0, "", DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, "")
    {
    }

    public bool HasError() => !string.IsNullOrWhiteSpace(ErrorMessage);
}
