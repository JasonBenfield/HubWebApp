import { AggregateComponent } from './AggregateComponent';
import { HtmlComponent } from "./HtmlComponent";
import { SmallViewModel } from './SmallViewModel';
export declare class Small extends HtmlComponent {
    constructor(vm?: SmallViewModel);
    protected readonly vm: SmallViewModel;
    readonly content: AggregateComponent;
    addContent<TItem extends IComponent>(item: TItem): TItem;
}
