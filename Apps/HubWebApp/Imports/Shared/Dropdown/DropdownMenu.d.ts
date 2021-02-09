import { ListItem } from "../Html/ListItem";
import { ListItemViewModel } from "../Html/ListItemViewModel";
import { UnorderedList } from "../Html/UnorderedList";
import { UnorderedListViewModel } from "../Html/UnorderedListViewModel";
export declare class DropdownMenu extends UnorderedList {
    constructor(createItem?: (itemVM: ListItemViewModel) => ListItem, createItemVM?: () => ListItemViewModel, vm?: UnorderedListViewModel);
}
