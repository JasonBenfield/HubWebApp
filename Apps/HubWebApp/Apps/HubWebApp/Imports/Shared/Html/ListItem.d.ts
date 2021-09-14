import { HtmlContainerComponent } from "./HtmlContainerComponent";
export declare class ListItem extends HtmlContainerComponent implements IListItem {
    constructor(vm?: IListItemViewModel);
    protected readonly vm: IListItemViewModel;
    private data;
    getData<T>(): T;
    setData(data: any): void;
    addToList(list: IList): this;
}
