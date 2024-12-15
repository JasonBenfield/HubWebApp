using XTI_Core;
using XTI_TempLog;
using XTI_TempLog.Abstractions;

namespace XTI_PermanentLog;

public sealed class TempToPermanentLog
{
    private readonly TempLog tempLog;
    private readonly IPermanentLog permanentLog;
    private readonly IClock clock;
    private readonly int processMinutesBefore = 1;

    public TempToPermanentLog(TempLog tempLog, IPermanentLog permanentLog, IClock clock, int processMinutesBefore = 1)
    {
        this.tempLog = tempLog;
        this.permanentLog = permanentLog;
        this.clock = clock;
        this.processMinutesBefore = processMinutesBefore;
    }

    public async Task Move()
    {
        var modifiedBefore = clock.Now().AddMinutes(-processMinutesBefore);
        var i = 0;
        var logFiles = tempLog.Files(modifiedBefore, 1000);
        while(logFiles.Length > 0 && i < 100)
        {
            var sessionDetails = new List<TempLogSessionDetailModel>();
            var filesToProcess = new List<ITempLogFile>();
            foreach(var logFile in logFiles)
            {
                ITempLogFile renamedFile;
                try
                {
                    renamedFile = logFile.WithNewName($"{logFile.Name}.processing");
                    filesToProcess.Add(renamedFile);
                }
                catch
                {
                    continue;
                }
                try
                {
                    var fileSessionDetails = await renamedFile.Read();
                    sessionDetails.AddRange(fileSessionDetails);
                }
                catch
                {
                    filesToProcess.Remove(renamedFile);
                }
            }
            await permanentLog.LogSessionDetails(sessionDetails.ToArray(), default);
            foreach(var fileToProcess in filesToProcess)
            {
                try
                {
                    fileToProcess.Delete();
                }
                catch
                {
                }
            }
            logFiles = tempLog.Files(modifiedBefore, 1000);
            i++;
        }
    }
}
