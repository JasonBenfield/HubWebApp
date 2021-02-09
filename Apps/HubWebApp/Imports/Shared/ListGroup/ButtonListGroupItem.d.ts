import { ButtonListItemViewModel } from "./ButtonListItemViewModel";
import { ListGroupItem } from "./ListGroupItem";
export declare class ButtonListGroupItem extends ListGroupItem {
    constructor(vm: ButtonListItemViewModel);
    private readonly button;
    readonly clicked: IEventHandler<any>;
    enable(): void;
    disable(): void;
}
