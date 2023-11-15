namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class GetLocalDateTime
{
    public static readonly string Sql = $@"
CREATE or ALTER FUNCTION [dbo].[GetLocalDateTime](
    @date datetimeoffset
)
RETURNS datetimeoffset
AS 
BEGIN
    RETURN case 
        when @date is null then '9999-12-31 23:59:59.9999999 +00:00'
        when year(@date) = 9999 then @date 
        else @date at time zone 'Eastern Standard Time'
        end
END;
";
}