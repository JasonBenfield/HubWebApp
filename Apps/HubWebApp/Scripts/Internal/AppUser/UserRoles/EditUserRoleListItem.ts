import { FaIcon } from "XtiShared/FaIcon";
import { Row } from "XtiShared/Grid/Row";
import { ButtonListItemViewModel } from "XtiShared/ListGroup/ButtonListItemViewModel";
import { ButtonListGroupItem } from "XtiShared/ListGroup/ButtonListGroupItem";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { ColumnCss } from "XtiShared/ColumnCss";
import { ContextualClass } from "XtiShared/ContextualClass";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";

export class EditUserRoleListItem extends ButtonListGroupItem {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: ButtonListItemViewModel = new ButtonListItemViewModel()
    ) {
        super(vm);
        let row = this.addContent(new Row());
        this.icon = row.addColumn()
            .configure(col => col.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new FaIcon('square'))
            .configure(icon => {
                icon.makeFixedWidth();
                icon.regularStyle();
            });
        this.roleName = row.addColumn()
            .addContent(new TextSpan(''));
        this.clicked.register(this.onClick.bind(this));
    }

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    private readonly roleName: TextSpan;

    private onClick() {
        return this.toggleAssignment();
    }

    private async toggleAssignment() {
        this.disable();
        this.icon.solidStyle();
        this.icon.setName('sync-alt');
        this.icon.startAnimation('spin');
        try {
            await this.hubApi.AppUserMaintenance.AssignRole({
                UserID: this.userID,
                RoleID: this.roleID
            });
            this.assignedIcon();
        }
        finally {
            this.enable();
            this.icon.stopAnimation();
        }
    }

    private readonly icon: FaIcon;

    private roleID: number;

    withAssignedRole(userRole: IAppRoleModel) {
        this.roleName.setText(userRole.Name);
        this.roleID = userRole.ID;
        this.assignedIcon();
    }

    private assignedIcon() {
        this.icon.regularStyle();
        this.icon.setName('check-square');
        this.icon.setColor(ContextualClass.success);
    }

    withUnassignedRole(role: IAppRoleModel) {
        this.roleName.setText(role.Name);
        this.roleID = role.ID;
        this.unassignedIcon();
    }

    private unassignedIcon() {
        this.icon.regularStyle();
        this.icon.setName('square');
        this.icon.setColor(ContextualClass.default);
    }

}