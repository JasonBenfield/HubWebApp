import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { RoleButtonListItemView } from "./RoleButtonListItemView";
import { AppRole } from "../../Lib/AppRole";

export class RoleListItem extends BasicComponent {
    constructor(readonly role: AppRole, view: RoleButtonListItemView) {
        super(view);
        new TextComponent(view.roleName).setText(role.name.displayText);
    }
} 