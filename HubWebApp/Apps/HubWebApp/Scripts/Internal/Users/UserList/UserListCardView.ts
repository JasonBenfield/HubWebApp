import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ButtonListGroupView, ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";
import { UserListItemView } from "./UserListItemView";

export class UserListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly users: ButtonListGroupView;

    constructor(container: BasicComponentView) {
        super(container);
        this.setViewName(UserListCardView.name);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.users = this.addView(ButtonListGroupView);
        this.users.setItemViewType(UserListItemView);
    }
}