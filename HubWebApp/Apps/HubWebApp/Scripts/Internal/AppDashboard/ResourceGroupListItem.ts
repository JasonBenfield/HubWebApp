import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ResourceGroupListItemView } from "./ResourceGroupListItemView";

export class ResourceGroupListItem extends BasicComponent {
    constructor(readonly group: IResourceGroupModel, view: ResourceGroupListItemView) {
        super(view);
        new TextComponent(view.groupName).setText(group.Name.DisplayText);
    }
}