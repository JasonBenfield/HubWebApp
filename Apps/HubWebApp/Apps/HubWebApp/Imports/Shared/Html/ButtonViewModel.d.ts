import * as ko from 'knockout';
import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import { AggregateComponentViewModel } from "./AggregateComponentViewModel";
export declare class ButtonViewModel extends HtmlComponentViewModel {
    constructor();
    readonly content: AggregateComponentViewModel;
    readonly type: ko.Observable<string>;
    readonly isEnabled: ko.Observable<boolean>;
    private readonly _clicked;
    readonly clicked: import("../Events").DefaultEventHandler<any>;
    click(): boolean;
}
