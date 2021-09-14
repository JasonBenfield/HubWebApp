import { Block } from "../Html/Block";
import { DropdownButton } from "./DropdownButton";
import { DropdownSpanItem } from "./DropdownSpanItem";
import { DropdownMenu } from "./DropdownMenu";
import { DropdownLinkItem } from "./DropdownLinkItem";
import { DropdownBlockViewModel } from "./DropdownBlockViewModel";
export declare class DropdownBlock extends Block {
    constructor(vm?: DropdownBlockViewModel);
    protected readonly vm: DropdownBlockViewModel;
    readonly button: DropdownButton;
    readonly menu: DropdownMenu;
    addSpanItem(): DropdownSpanItem;
    addLinkItem(): DropdownLinkItem;
}
