using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record UserAccessModel(bool HasAccess, AppRoleModel[] AssignedRoles);