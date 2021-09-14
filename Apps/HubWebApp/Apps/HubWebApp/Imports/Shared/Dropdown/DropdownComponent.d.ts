import { DropdownButton } from "./DropdownButton";
import { DropdownSpanItem } from "./DropdownSpanItem";
import { DropdownMenu } from "./DropdownMenu";
import { DropdownLinkItem } from "./DropdownLinkItem";
import { DropdownComponentViewModel } from "./DropdownComponentViewModel";
export declare class DropdownComponent implements IComponent {
    private readonly vm;
    constructor(vm?: DropdownComponentViewModel);
    addToContainer(container: IAggregateComponent): any;
    insertIntoContainer(container: IAggregateComponent, index: number): this;
    removeFromContainer(container: IAggregateComponent): any;
    readonly button: DropdownButton;
    readonly menu: DropdownMenu;
    addSpanItem(): DropdownSpanItem;
    addLinkItem(): DropdownLinkItem;
    show(): void;
    hide(): void;
}
