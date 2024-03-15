namespace XTI_HubDB.EF.SqlServer.Migrations.V1423;

internal static class InitialTimeLoggedIn
{
    public static readonly string Sql = @"
update Users set TimeLoggedIn = dbo.GetMaxDateTime();

update Users
set TimeLoggedIn =
(
    select 
        top 1
        TimeStarted
    from Sessions
    where
        Users.ID = Sessions.UserID
    order by TimeStarted desc
)
where 
exists
(
    select TimeStarted
    from Sessions
    where
        Users.ID = Sessions.UserID
);
";
}
