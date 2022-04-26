import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { AppListItemView } from "./AppListItemView";

export class AppListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly apps: ListGroupView;

    constructor() {
        super();
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.apps = this.addBlockListGroup(() => new AppListItemView());
    }
}