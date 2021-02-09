export declare class AggregateComponent implements IAggregateComponent, IComponent {
    private readonly vm;
    constructor(vm: IAggregateComponentViewModel);
    setName(name: string): void;
    private readonly items;
    configure(action: (c: this) => void): this;
    addToContainer(container: IAggregateComponent): this;
    insertIntoContainer(container: IAggregateComponent, index: number): this;
    removeFromContainer(container: IAggregateComponent): this;
    clear(): void;
    prependItem<TItem extends IComponent, TItemVM extends IComponentViewModel>(itemVM: TItemVM, create: (vm: TItemVM) => TItem): TItem;
    insertItemBefore<TItem extends IComponent>(otherItem: IComponent, item: TItem): TItem;
    insertItemAfter<TItem extends IComponent>(otherItem: IComponent, item: TItem): TItem;
    moveItemBefore(sourceItem: IComponent, targetItem: IComponent): IComponent;
    moveItemAfter(sourceItem: IComponent, targetItem: IComponent): IComponent;
    private indexOf;
    addContent<TItem extends IComponent>(item: TItem): TItem;
    insertContent<TItem extends IComponent>(index: number, item: TItem): TItem;
    addItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(itemVM: TItemVM, item: TItem): TItem;
    insertItem<TItemVM extends IComponentViewModel, TItem extends IComponent>(index: number, itemVM: TItemVM, item: TItem): TItem;
    removeItem<TItem extends IComponent>(item: TItem): void;
    private splice;
    show(): void;
    hide(): void;
}
