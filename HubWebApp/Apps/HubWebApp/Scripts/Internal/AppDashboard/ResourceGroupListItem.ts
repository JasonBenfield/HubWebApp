import { BasicComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/BasicComponent";
import { TextComponent } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/TextComponent";
import { ResourceGroupListItemView } from "./ResourceGroupListItemView";

export class ResourceGroupListItem extends BasicComponent {
    constructor(readonly group: IResourceGroupModel, view: ResourceGroupListItemView) {
        super(view);
        new TextComponent(view.groupName).setText(group.Name);
    }
}