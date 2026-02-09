using XTI_Hub.Abstractions;

namespace XTI_Installation;

public sealed class AppCommandStepException : Exception
{
    public AppCommandStepException(AppCommandStepModel step)
        : base($"Error running step {step.ID}\r\n{step.ErrorMessage}")
    {
        Step = step;
    }

    public AppCommandStepModel Step { get; }
}
