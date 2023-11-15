namespace XTI_HubDB.EF.SqlServer.V1423;

internal static class ExpandedModifiersView
{
    public static readonly string Sql = """
CREATE OR ALTER view ExpandedModifiers as
select 
	mod.ID ModifierID, mod.ModKeyDisplayText ModKey, mod.TargetKey ModTargetKey, mod.DisplayText ModDisplayText,
	modcat.ID ModCategoryID, modcat.DisplayText ModCategoryName,
	Apps.ID AppID, 
	dbo.AppKeyDisplayText(Apps.DisplayText, Apps.Type) AppKey,
	Apps.DisplayText AppName, dbo.GetLocalDateTime(Apps.TimeAdded) TimeAppAdded, Apps.Title AppTitle,
	dbo.AppTypeDisplayText(Apps.Type) AppType, 
	Apps.Type AppTypeValue
from Modifiers mod
inner join ModifierCategories modcat
on mod.CategoryID = modcat.ID
inner join Apps
on modcat.AppID = Apps.ID
""";
}
