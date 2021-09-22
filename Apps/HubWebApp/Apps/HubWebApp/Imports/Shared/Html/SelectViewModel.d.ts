import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import * as ko from 'knockout';
import { SelectOption } from "./SelectOption";
export declare class SelectViewModel extends HtmlComponentViewModel {
    constructor();
    readonly isEnabled: ko.Observable<boolean>;
    readonly value: ko.Observable<any>;
    readonly items: ko.ObservableArray<SelectOption<any>>;
    readonly itemsText: ko.Observable<string>;
    readonly itemsValue: ko.Observable<string>;
    readonly itemsCaption: ko.Observable<string>;
    readonly hasFocus: ko.Observable<boolean>;
}
