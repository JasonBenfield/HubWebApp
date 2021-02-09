import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import * as ko from 'knockout';
export declare class TextSpanViewModel extends HtmlComponentViewModel {
    constructor();
    readonly text: ko.Observable<string>;
}
