import { ListBlockViewModel } from "../Html/ListBlockViewModel";
import { BaseListGroup } from "./BaseListGroup";
import { LinkListGroupItem } from "./LinkListGroupItem";
import { LinkListItemViewModel } from "./LinkListItemViewModel";
export declare class LinkListGroup extends BaseListGroup {
    constructor(createItem?: (itemVM: LinkListItemViewModel) => LinkListGroupItem, createItemVM?: () => LinkListItemViewModel, vm?: ListBlockViewModel);
    setItems: <TSourceItem>(sourceItems: TSourceItem[], config: (sourceItem: TSourceItem, listItem: LinkListGroupItem) => void) => void;
}
