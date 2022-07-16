import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { RoleButtonListItemView } from "./RoleButtonListItemView";

export class RoleListItem extends BasicComponent {
    constructor(readonly role: IAppRoleModel, view: RoleButtonListItemView) {
        super(view);
        new TextComponent(view.roleName).setText(role.Name.DisplayText);
    }
} 