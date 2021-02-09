import { BlockViewModel } from "../Html/BlockViewModel";
import { ButtonViewModel } from "../Html/ButtonViewModel";
import { UnorderedListViewModel } from "../Html/UnorderedListViewModel";
export declare class DropdownBlockViewModel extends BlockViewModel {
    constructor();
    readonly button: ButtonViewModel;
    readonly menu: UnorderedListViewModel;
}
