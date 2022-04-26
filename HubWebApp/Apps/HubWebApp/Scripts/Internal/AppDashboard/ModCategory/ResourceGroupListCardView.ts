import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";

export class ResourceGroupListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly requests: ListGroupView;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.requests = this.addBlockListGroup();
    }
}