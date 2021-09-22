import { Link } from "../Html/Link";
import { ListItem } from "../Html/ListItem";
import { ListItemViewModel } from "../Html/ListItemViewModel";
export declare class DropdownLinkItem extends ListItem {
    constructor(vm?: ListItemViewModel);
    readonly link: Link;
}
