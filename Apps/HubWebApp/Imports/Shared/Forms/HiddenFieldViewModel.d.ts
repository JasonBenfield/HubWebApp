import * as ko from 'knockout';
import { ComponentViewModel } from "../ComponentViewModel";
export declare class HiddenFieldViewModel extends ComponentViewModel {
    constructor();
    readonly name: ko.Observable<string>;
    readonly value: ko.Observable<any>;
}
