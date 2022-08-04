namespace XTI_HubWebAppApi.AppUserInquiry;

public sealed record UserAccessModel(bool HasAccess, AppRoleModel[] AssignedRoles);