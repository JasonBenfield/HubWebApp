import { ListItemViewModel } from "../Html/ListItemViewModel";
import { UnorderedListViewModel } from "../Html/UnorderedListViewModel";
import { BaseListGroup } from "./BaseListGroup";
import { ListGroupItem } from "./ListGroupItem";
export declare class ListGroup extends BaseListGroup {
    constructor(createItem?: (itemVM: ListItemViewModel) => ListGroupItem, createItemVM?: () => ListItemViewModel, vm?: UnorderedListViewModel);
}
