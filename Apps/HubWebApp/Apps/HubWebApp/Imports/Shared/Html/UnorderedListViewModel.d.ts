import * as ko from 'knockout';
import { ListItemViewModel } from "./ListItemViewModel";
import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
export declare class UnorderedListViewModel extends HtmlComponentViewModel implements IListViewModel {
    constructor();
    readonly items: ko.ObservableArray<ListItemViewModel>;
    readonly hasItems: ko.Observable<boolean>;
    private readonly _itemClicked;
    readonly itemClicked: import("../Events").DefaultEventHandler<ListItemViewModel>;
    click(listItem: ListItemViewModel): void;
}
