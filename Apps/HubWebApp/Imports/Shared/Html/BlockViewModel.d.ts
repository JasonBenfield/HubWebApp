import { HtmlComponentViewModel } from "./HtmlComponentViewModel";
import { AggregateComponentViewModel } from "./AggregateComponentViewModel";
export declare class BlockViewModel extends HtmlComponentViewModel implements IHtmlContainerComponentViewModel {
    constructor();
    readonly content: AggregateComponentViewModel;
}
