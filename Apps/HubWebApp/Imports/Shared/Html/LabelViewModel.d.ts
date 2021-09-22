import * as ko from 'knockout';
import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import { AggregateComponentViewModel } from "./AggregateComponentViewModel";
export declare class LabelViewModel extends HtmlComponentViewModel {
    constructor();
    readonly content: AggregateComponentViewModel;
    readonly forTarget: ko.Observable<string>;
}
