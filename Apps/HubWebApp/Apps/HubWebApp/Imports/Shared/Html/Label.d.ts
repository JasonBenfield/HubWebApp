import { HtmlComponent } from "./HtmlComponent";
import { AggregateComponent } from './AggregateComponent';
import { LabelViewModel } from './LabelViewModel';
export declare class Label extends HtmlComponent {
    constructor(vm?: LabelViewModel);
    protected readonly vm: LabelViewModel;
    readonly content: AggregateComponent;
    setFor(forTarget: string): void;
    addContent<TItem extends IComponent>(item: TItem): TItem;
}
