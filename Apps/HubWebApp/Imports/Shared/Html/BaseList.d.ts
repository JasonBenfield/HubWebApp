import { HtmlComponent } from "./HtmlComponent";
export declare class BaseList extends HtmlComponent implements IList {
    private readonly createItem;
    private readonly createItemVM;
    constructor(createItem: (itemVM: IListItemViewModel) => IListItem, createItemVM: () => IListItemViewModel, vm: IListViewModel);
    private onItemClicked;
    protected readonly vm: IListViewModel;
    readonly items: IListItem[];
    private readonly _itemClicked;
    readonly itemClicked: import("../Events").DefaultEventHandler<IListItem>;
    clear(): void;
    addItem(): IListItem;
    add<TListItem extends IListItem>(itemVM: IListItemViewModel, create: (vm: IListItemViewModel) => TListItem): TListItem;
    addListItem<TListItem extends IListItem>(itemVM: IListItemViewModel, item: TListItem): TListItem;
    setItems<TSourceItem>(sourceItems: TSourceItem[], config: (sourceItem: TSourceItem, listItem: IListItem) => void): void;
}
