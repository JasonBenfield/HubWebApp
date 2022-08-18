namespace XTI_HubWebAppApi.System;

public sealed class AddOrUpdateModifierByTargetKeyRequest
{
    public string ModCategoryName { get; set; } = "";
    public GenerateKeyModel GenerateModKey { get; set; } = GenerateKeyModel.Guid();
    public string TargetKey { get; set; } = "";
    public string TargetDisplayText { get; set; } = "";
}
