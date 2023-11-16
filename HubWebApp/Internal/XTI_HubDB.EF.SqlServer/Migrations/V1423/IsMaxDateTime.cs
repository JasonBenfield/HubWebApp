namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class IsMaxDatTime
{
    public static readonly string Sql = $@"
CREATE or ALTER FUNCTION [dbo].[IsMaxDateTime](
    @date datetimeoffset
)
RETURNS bit
AS 
BEGIN
    RETURN case 
        when @date is null then null
        when year(@date) = 9999 then 1
        else 0
        end
END;
";
}