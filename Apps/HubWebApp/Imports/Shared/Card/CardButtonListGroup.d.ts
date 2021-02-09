import { ListBlockViewModel } from "../Html/ListBlockViewModel";
import { ButtonListGroup } from "../ListGroup/ButtonListGroup";
import { ButtonListGroupItem } from "../ListGroup/ButtonListGroupItem";
import { ButtonListItemViewModel } from "../ListGroup/ButtonListItemViewModel";
export declare class CardButtonListGroup extends ButtonListGroup {
    constructor(createItem?: (itemVM: ButtonListItemViewModel) => ButtonListGroupItem, createItemVM?: () => ButtonListItemViewModel, vm?: ListBlockViewModel);
}
