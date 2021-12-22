import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EditUserRoleListItemView } from "./EditUserRoleListItemView";

export class EditUserRoleListItem {
    private userID: number;
    private roleID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: EditUserRoleListItemView
    ) {
        this.view.clicked.register(this.onClick.bind(this));
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    private onClick() {
        return this.toggleAssignment();
    }

    private async toggleAssignment() {
        this.view.startAssignment();
        try {
            await this.hubApi.AppUserMaintenance.AssignRole({
                UserID: this.userID,
                RoleID: this.roleID
            });
            this.view.assign();
        }
        finally {
            this.view.endAssignment();
        }
    }

    withAssignedRole(userRole: IAppRoleModel) {
        this.view.setRoleName(userRole.Name);
        this.roleID = userRole.ID;
        this.view.assign();
    }

    withUnassignedRole(role: IAppRoleModel) {
        this.view.setRoleName(role.Name);
        this.roleID = role.ID;
        this.view.unassign();
    }
}