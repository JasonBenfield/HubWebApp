import { Span } from "../Html/Span";
import { ListItem } from "../Html/ListItem";
import { ListItemViewModel } from "../Html/ListItemViewModel";
export declare class DropdownSpanItem extends ListItem {
    constructor(vm?: ListItemViewModel);
    readonly span: Span;
}
