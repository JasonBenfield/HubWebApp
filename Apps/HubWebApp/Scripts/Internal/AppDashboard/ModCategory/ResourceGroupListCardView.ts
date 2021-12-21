import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";

export class ResourceGroupListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    readonly requests: ListGroupView;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        this.requests = this.addBlockListGroup();
    }
}