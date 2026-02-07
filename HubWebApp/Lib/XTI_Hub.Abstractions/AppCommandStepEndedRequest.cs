namespace XTI_Hub.Abstractions;

public sealed class AppCommandStepEndedRequest
{
    public AppCommandStepEndedRequest()
        : this(0, "")
    {
    }

    public AppCommandStepEndedRequest(int stepID, string errorMessage)
    {
        StepID = stepID;
        ErrorMessage = errorMessage;
    }

    public int StepID { get; set; }
    public string ErrorMessage { get; set; }
}
