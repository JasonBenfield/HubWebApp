namespace XTI_HubDB.Entities;

public sealed class AppCommandStepEntity
{
    public int ID { get; set; }
    public int RequestedInstallationID { get; set; }
    public string Activity { get; set; } = "";
    public DateTimeOffset TimeStarted { get; set; } = DateTimeOffset.MaxValue;
    public DateTimeOffset TimeEnded { get; set; } = DateTimeOffset.MaxValue;
    public string ErrorMessage { get; set; } = "";
}
