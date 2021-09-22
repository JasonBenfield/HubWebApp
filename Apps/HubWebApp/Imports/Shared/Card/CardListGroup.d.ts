import { ListItemViewModel } from "../Html/ListItemViewModel";
import { UnorderedListViewModel } from "../Html/UnorderedListViewModel";
import { ListGroup } from "../ListGroup/ListGroup";
import { ListGroupItem } from "../ListGroup/ListGroupItem";
export declare class CardListGroup extends ListGroup {
    constructor(createItem?: (itemVM: ListItemViewModel) => ListGroupItem, createItemVM?: () => ListItemViewModel, vm?: UnorderedListViewModel);
}
