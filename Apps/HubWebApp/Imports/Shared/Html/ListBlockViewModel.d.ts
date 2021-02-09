import * as ko from 'knockout';
import { HtmlComponentViewModel } from "../Html/HtmlComponentViewModel";
export declare class ListBlockViewModel extends HtmlComponentViewModel implements IListViewModel {
    constructor();
    private readonly _itemClicked;
    readonly itemClicked: import("../Events").DefaultEventHandler<IListItemViewModel>;
    readonly items: ko.ObservableArray<IListItemViewModel>;
    readonly hasItems: ko.Observable<boolean>;
    click(item: IListItemViewModel): void;
}
