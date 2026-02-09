using XTI_App.Abstractions;
using XTI_Core;
using XTI_Internal.Abstractions;
using XTI_TempLog.Abstractions;

namespace XTI_Hub;

public sealed class EfPermanentLog : IPermanentLog
{
    private readonly EfHubDB db;

    public EfPermanentLog(EfHubDB db)
    {
        this.db = db;
    }

    public async Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct)
    {
        foreach (var sessionDetailRequest in sessionDetails)
        {
            var efUser = await db.Users.UserOrAnon(new AppUserName(sessionDetailRequest.Session.SessionKey.UserName), ct);
            var efSession = await db.Sessions.AddOrUpdate
            (
                sessionKey: sessionDetailRequest.Session.SessionKey.ID,
                efUser: efUser,
                timeStarted: sessionDetailRequest.Session.TimeStarted,
                timeEnded: sessionDetailRequest.Session.TimeEnded,
                requesterKey: sessionDetailRequest.Session.RequesterKey,
                userAgent: sessionDetailRequest.Session.UserAgent,
                remoteAddress: sessionDetailRequest.Session.RemoteAddress,
                ct: ct
            );
            foreach (var requestDetail in sessionDetailRequest.RequestDetails)
            {
                var efInstallation = await db.Installations.InstallationOrDefault(requestDetail.Request.InstallationID, ct);
                var efRequest = await efSession.LogRequest
                (
                    requestKey: requestDetail.Request.RequestKey,
                    installation: efInstallation,
                    path: requestDetail.Request.Path,
                    timeStarted: requestDetail.Request.TimeStarted,
                    timeEnded: requestDetail.Request.TimeEnded,
                    actualCount: requestDetail.Request.ActualCount,
                    sourceRequestKey: requestDetail.Request.SourceRequestKey,
                    requestData: requestDetail.Request.RequestData,
                    resultData: requestDetail.Request.ResultData,
                    ct: ct
                );
                foreach (var logEntry in requestDetail.LogEntries)
                {
                    await efRequest.LogEvent
                    (
                        logEntryKey: logEntry.EventKey,
                        severity: AppEventSeverity.Values.Value(logEntry.Severity),
                        timeOccurred: logEntry.TimeOccurred,
                        caption: logEntry.Caption,
                        message: logEntry.Message,
                        detail: logEntry.Detail,
                        actualCount: logEntry.ActualCount,
                        sourceLogEntryKey: logEntry.ParentEventKey,
                        category: logEntry.Category,
                        ct: ct
                    );
                }
            }
        }
    }
}
