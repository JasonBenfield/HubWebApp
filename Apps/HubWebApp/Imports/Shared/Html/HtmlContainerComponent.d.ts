import { HtmlComponent } from "./HtmlComponent";
export declare class HtmlContainerComponent extends HtmlComponent implements IAggregateComponent {
    protected readonly vm: IHtmlContainerComponentViewModel;
    readonly content: IAggregateComponent;
    constructor(vm: IHtmlContainerComponentViewModel, content?: IAggregateComponent);
    addContent<TItem extends IComponent>(item: TItem): TItem;
    insertContent<TItem extends IComponent>(index: number, item: TItem): TItem;
    addItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(itemVM: TItemVM, item: TItem): TItem;
    insertItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(index: number, itemVM: TItemVM, item: TItem): TItem;
    removeItem<TItem extends IComponent>(item: TItem): any;
    addToContainer(container: IAggregateComponent): this;
    insertIntoContainer(container: IAggregateComponent, index: number): this;
    removeFromContainer(container: IAggregateComponent): this;
}
