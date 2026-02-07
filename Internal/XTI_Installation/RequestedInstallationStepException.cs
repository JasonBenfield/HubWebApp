using XTI_Hub.Abstractions;

namespace XTI_Installation;

public sealed class RequestedInstallationStepException : Exception
{
    public RequestedInstallationStepException(AppCommandStepModel step)
        : base($"Error running step {step.ID}\r\n{step.ErrorMessage}")
    {
        Step = step;
    }

    public AppCommandStepModel Step { get; }
}
