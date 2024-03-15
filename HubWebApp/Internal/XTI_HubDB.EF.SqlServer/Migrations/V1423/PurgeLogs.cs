namespace XTI_HubDB.EF.SqlServer.Migrations.V1423;

internal static class PurgeLogs
{
    public static readonly string Sql = @"
create or alter procedure dbo.purge_logs
as
begin
declare @max_date AS DATE = dateadd(day, -90, getDate());

delete SourceLogEntries
where exists 
(
    SELECT *
    FROM LogEntries le
    inner join Requests req
    ON le.RequestID = req.ID
    inner join Sessions s
    ON req.SessionID = s.ID
    where 
        SourceLogEntries.SourceID = le.ID AND 
        s.TimeStarted < @max_date
);

delete SourceLogEntries
where 
exists 
(
    SELECT *
    FROM LogEntries AS le
    inner join Requests req
    ON le.RequestID = req.ID
    inner join Sessions s
    ON req.SessionID = s.ID
    where 
        SourceLogEntries.TargetID = le.ID AND 
        s.TimeStarted < @max_date
);

delete LogEntries
where 
exists 
(
    SELECT *
    FROM Requests AS req
    inner join Sessions AS s
    ON req.SessionID = s.ID
    where 
        LogEntries.RequestID = req.ID AND 
        s.TimeStarted < @max_date
);

delete SourceRequests
where 
exists 
(
    SELECT *
    FROM Requests r
    inner join Sessions s
    on r.SessionID = s.ID
    WHERE 
        r.ID = SourceRequests.SourceID and
        s.TimeStarted < @max_date
);

delete SourceRequests
where 
exists 
(
    SELECT *
    FROM Requests r
    inner join Sessions s
    on r.SessionID = s.ID
    WHERE 
        r.ID = SourceRequests.TargetID and
        s.TimeStarted < @max_date
);

delete Requests
where 
exists 
(
    SELECT *
    FROM Sessions s
    where 
        Requests.SessionID = s.ID and 
        s.TimeStarted < @max_date
);

delete Sessions
where TimeStarted < @max_date;

end;
";
}
