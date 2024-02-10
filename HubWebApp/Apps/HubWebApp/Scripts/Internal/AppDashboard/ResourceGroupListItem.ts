import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ResourceGroupListItemView } from "./ResourceGroupListItemView";
import { AppResourceGroup } from "../../Lib/AppResourceGroup";

export class ResourceGroupListItem extends BasicComponent {
    constructor(readonly group: AppResourceGroup, view: ResourceGroupListItemView) {
        super(view);
        new TextComponent(view.groupName).setText(group.name.displayText);
    }
}