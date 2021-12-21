import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EditUserModifierListItemView } from "./EditUserModifierListItemView";

export class EditUserModifierListItem {
    private userID: number;
    private roleID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: EditUserModifierListItemView
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
            if (this.roleID) {
                let request: IUserRoleRequest = {
                    UserID: this.userID,
                    RoleID: this.roleID
                };
                await this.hubApi.AppUserMaintenance.UnassignRole(request);
                this.view.unassign();
            }
            else {
                this.roleID = await this.hubApi.AppUserMaintenance.AssignRole({
                    UserID: this.userID,
                    RoleID: this.roleID
                });
                this.view.assign();
            }
        }
        finally {
            this.view.endAssignment();
        }
    }

    withAssignedModifier(userRole: IAppRoleModel) {
        this.view.setModKey(userRole.Name);
        this.roleID = userRole.ID;
        this.view.assign();
    }

    withUnassignedModifier(modifier: IModifierModel) {
        this.view.setModKey(modifier.ModKey);
        this.view.setModDisplayText(modifier.DisplayText);
        this.roleID = null;
        this.roleID = modifier.ID;
        this.view.unassign();
    }
}