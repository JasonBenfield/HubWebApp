import { ContextualClass } from "../ContextualClass";
import { AggregateComponent } from "../Html/AggregateComponent";
import { HtmlComponent } from "../Html/HtmlComponent";
export declare class ListGroupItem extends HtmlComponent implements IListItem {
    constructor(vm: IListItemViewModel);
    private data;
    getData<T>(): T;
    setData(data: any): void;
    addToList(list: IList): this;
    readonly content: AggregateComponent;
    addContent<TItem extends IComponent>(item: TItem): TItem;
    protected readonly vm: IListItemViewModel;
    private contextClass;
    setContext(contextClass: ContextualClass): void;
    private getCss;
    private active;
    activate(): void;
    deactivate(): void;
    private setActive;
}
