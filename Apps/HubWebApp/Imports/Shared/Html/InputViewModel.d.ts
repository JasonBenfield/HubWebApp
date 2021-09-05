import * as ko from 'knockout';
import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
export declare class InputViewModel extends HtmlComponentViewModel {
    constructor();
    readonly type: ko.Observable<string>;
    readonly value: ko.Observable<string>;
    readonly maxLength: ko.Observable<number>;
    readonly isEnabled: ko.Observable<boolean>;
    readonly hasFocus: ko.Observable<boolean>;
    readonly autocomplete: ko.Observable<string>;
}
