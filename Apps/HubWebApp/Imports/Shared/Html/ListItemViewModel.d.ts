import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import { AggregateComponentViewModel } from './AggregateComponentViewModel';
export declare class ListItemViewModel extends HtmlComponentViewModel implements IListItemViewModel {
    constructor();
    readonly content: AggregateComponentViewModel;
    readonly isClickable = false;
}
