import { Block } from "../Html/Block";
import { BlockViewModel } from "../Html/BlockViewModel";
import { ListItemViewModel } from "../Html/ListItemViewModel";
import { ButtonListGroupItem } from "../ListGroup/ButtonListGroupItem";
import { ButtonListItemViewModel } from "../ListGroup/ButtonListItemViewModel";
import { LinkListGroupItem } from "../ListGroup/LinkListGroupItem";
import { LinkListItemViewModel } from "../ListGroup/LinkListItemViewModel";
import { ListGroupItem } from "../ListGroup/ListGroupItem";
import { CardAlert } from "./CardAlert";
import { CardBody } from "./CardBody";
import { CardButtonListGroup } from "./CardButtonListGroup";
import { CardHeader } from "./CardHeader";
import { CardLinkListGroup } from "./CardLinkListGroup";
import { CardListGroup } from "./CardListGroup";
import { CardTitleHeader } from "./CardTitleHeader";
export declare class Card extends Block {
    constructor(vm?: BlockViewModel);
    addCardTitleHeader(title: string): CardTitleHeader;
    addCardHeader(): CardHeader;
    addCardAlert(): CardAlert;
    addCardBody(): CardBody;
    addButtonListGroup(createItem?: (itemVM: ButtonListItemViewModel) => ButtonListGroupItem): CardButtonListGroup;
    addLinkListGroup(createItem?: (itemVM: LinkListItemViewModel) => LinkListGroupItem): CardLinkListGroup;
    addListGroup(createItem?: (itemVM: ListItemViewModel) => ListGroupItem): CardListGroup;
}
