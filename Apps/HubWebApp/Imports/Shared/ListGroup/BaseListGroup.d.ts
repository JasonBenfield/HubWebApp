import { BaseList } from "../Html/BaseList";
import { ListGroupItem } from "./ListGroupItem";
export declare class BaseListGroup extends BaseList {
    constructor(createItem: (itemVM: IListItemViewModel) => ListGroupItem, createItemVM: () => IListItemViewModel, vm: IListViewModel);
    makeFlush(): void;
}
