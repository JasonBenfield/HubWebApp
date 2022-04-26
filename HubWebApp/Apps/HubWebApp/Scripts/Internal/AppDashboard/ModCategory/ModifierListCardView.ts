import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { ModifierListItemView } from "./ModifierListItemView";

export class ModifierListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly modifiers: ListGroupView;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.modifiers = this.addBlockListGroup(() => new ModifierListItemView());
    }
}