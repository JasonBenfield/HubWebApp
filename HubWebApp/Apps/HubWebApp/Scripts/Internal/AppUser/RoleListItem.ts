import { BasicComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/BasicComponent";
import { TextComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/TextComponent";
import { RoleButtonListItemView } from "./RoleButtonListItemView";

export class RoleListItem extends BasicComponent {
    constructor(readonly role: IAppRoleModel, view: RoleButtonListItemView) {
        super(view);
        new TextComponent(view.roleName).setText(role.Name);
    }
} 