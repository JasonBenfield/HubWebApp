import { ResourceGroupListItemView } from "./ResourceGroupListItemView";

export class ResourceGroupListItem {
    constructor(readonly group: IResourceGroupModel, view: ResourceGroupListItemView) {
        view.setGroupName(group.Name);
    }
}