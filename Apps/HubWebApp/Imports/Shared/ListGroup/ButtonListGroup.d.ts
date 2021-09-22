import { ListBlockViewModel } from "../Html/ListBlockViewModel";
import { BaseListGroup } from "./BaseListGroup";
import { ButtonListGroupItem } from "./ButtonListGroupItem";
import { ButtonListItemViewModel } from "./ButtonListItemViewModel";
export declare class ButtonListGroup extends BaseListGroup {
    constructor(createItem?: (itemVM: ButtonListItemViewModel) => ButtonListGroupItem, createItemVM?: () => ButtonListItemViewModel, vm?: ListBlockViewModel);
}
