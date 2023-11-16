namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class GetMaxDateTime
{
    public static readonly string Sql = $@"
CREATE or ALTER FUNCTION [dbo].[GetMaxDateTime]()
RETURNS datetimeoffset
AS 
BEGIN
    RETURN '9999-12-31 23:59:59.9999999 +00:00'
END;
";
}