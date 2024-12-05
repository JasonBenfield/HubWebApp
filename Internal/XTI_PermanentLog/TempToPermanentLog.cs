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
                catch(Exception ex)
                {
                    throw new Exception($"Error renaming file '{logFile.Name}'.", ex);
                }
                try
                {
                    var fileSessionDetails = await renamedFile.Read();
                    sessionDetails.AddRange(fileSessionDetails);
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error reading file '{logFile.Name}'.", ex);
                }
            }
            await permanentLog.LogSessionDetails(sessionDetails.ToArray(), default);
            foreach(var fileToProcess in filesToProcess)
            {
                try
                {
                    fileToProcess.Delete();
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error deleting file '{fileToProcess.Name}'.", ex);
                }
            }
            logFiles = tempLog.Files(modifiedBefore, 1000);
            i++;
        }
    }
}
