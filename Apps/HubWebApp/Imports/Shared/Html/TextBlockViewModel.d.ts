import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import * as ko from 'knockout';
export declare class TextBlockViewModel extends HtmlComponentViewModel {
    constructor();
    readonly text: ko.Observable<string>;
}
