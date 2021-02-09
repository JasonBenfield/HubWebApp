import { ComponentViewModel } from "../ComponentViewModel";
import { ButtonViewModel } from "../Html/ButtonViewModel";
import { UnorderedListViewModel } from "../Html/UnorderedListViewModel";
import * as ko from 'knockout';
export declare class DropdownComponentViewModel extends ComponentViewModel {
    constructor();
    readonly isVisible: ko.Observable<boolean>;
    readonly button: ButtonViewModel;
    readonly menu: UnorderedListViewModel;
}
