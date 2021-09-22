import { BaseList } from "./BaseList";
import { ListItem } from "./ListItem";
import { ListItemViewModel } from "./ListItemViewModel";
import { UnorderedListViewModel } from "./UnorderedListViewModel";
export declare class UnorderedList extends BaseList {
    constructor(createItem?: (itemVM: ListItemViewModel) => ListItem, createItemVM?: () => ListItemViewModel, vm?: UnorderedListViewModel);
    protected readonly vm: UnorderedListViewModel;
}
