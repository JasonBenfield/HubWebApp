using XTI_Hub;

namespace XTI_HubAppApi.AppUserInquiry;

public sealed record UserAccessModel(bool HasAccess, AppRoleModel[] AssignedRoles);