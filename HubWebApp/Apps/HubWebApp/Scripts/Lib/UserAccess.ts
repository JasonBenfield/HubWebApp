import { AppRole } from "./AppRole";

export class UserAccess {
    readonly hasAccess: boolean;
    readonly assignedRoles: AppRole[];

    constructor(source: IUserAccessModel) {
        this.hasAccess = source.HasAccess;
        this.assignedRoles = source.AssignedRoles.map(r => new AppRole(r));
    }
}