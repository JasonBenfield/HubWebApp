import { BlockViewModel } from "./BlockViewModel";
import { Container } from "./Container";
import { HtmlComponent } from "./HtmlComponent";
export declare class FlexColumnFill extends HtmlComponent {
    constructor(vm?: BlockViewModel);
    readonly container: Container;
    addContent<TItem extends IComponent>(item: TItem): TItem;
}
