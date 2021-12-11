param ([Parameter(Mandatory)]$Name)
$env:DOTNET_ENVIRONMENT="Development"
dotnet ef --startup-project ./Tools/HubDbTool migrations add $Name --project ./Internal/XTI_HubDB.EF.SqlServer