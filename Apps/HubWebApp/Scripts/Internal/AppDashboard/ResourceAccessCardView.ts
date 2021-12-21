import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { RoleAccessListItemView } from "./RoleAccessListItemView";

export class ResourceAccessCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: MessageAlertView;
    readonly accessItems: ListGroupView;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert().alert;
        this.accessItems = this.addUnorderedListGroup(() => new RoleAccessListItemView());
    }
}