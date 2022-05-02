import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ResourceGroupListItemView } from "./ResourceGroupListItemView";

export class ResourceGroupListItem {
    constructor(readonly group: IResourceGroupModel, view: ResourceGroupListItemView) {
        new TextBlock(group.Name, view.groupName);
    }
}