import { AppRole } from "./AppRole";

export class UserAccess {
    readonly hasAccess: boolean;
    readonly assignedRoles: AppRole[];

    constructor(source?: IUserAccessModel) {
        this.hasAccess = source ? source.HasAccess : false;
        this.assignedRoles = source ? source.AssignedRoles.map(r => new AppRole(r)) : [];
    }
}