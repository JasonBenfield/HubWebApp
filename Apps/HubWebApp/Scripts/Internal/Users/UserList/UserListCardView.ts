import { CardAlertView } from "@jasonbenfield/sharedwebapp/Card/CardAlertView";
import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroupView";
import { UserListItemView } from "./UserListItemView";

export class UserListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly users: ListGroupView;

    constructor() {
        super();
        this.setName(UserListCardView.name);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.users = this.addBlockListGroup(() => new UserListItemView());
    }
}