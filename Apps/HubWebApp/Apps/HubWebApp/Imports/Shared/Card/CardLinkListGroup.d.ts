import { ListBlockViewModel } from "../Html/ListBlockViewModel";
import { LinkListGroup } from "../ListGroup/LinkListGroup";
import { LinkListGroupItem } from "../ListGroup/LinkListGroupItem";
import { LinkListItemViewModel } from "../ListGroup/LinkListItemViewModel";
export declare class CardLinkListGroup extends LinkListGroup {
    constructor(createItem?: (itemVM: LinkListItemViewModel) => LinkListGroupItem, createItemVM?: () => LinkListItemViewModel, vm?: ListBlockViewModel);
}
