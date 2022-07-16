import { CardAlertView, CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { ListGroupView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { BasicComponentView } from "@jasonbenfield/sharedwebapp/Views/BasicComponentView";

export class ResourceGroupListCardView extends CardView {
    readonly titleHeader: CardTitleHeaderView;
    readonly alert: CardAlertView;
    readonly requests: ListGroupView;

    constructor(container: BasicComponentView) {
        super(container);
        this.titleHeader = this.addCardTitleHeader();
        this.alert = this.addCardAlert();
        this.requests = this.addView(ListGroupView);
    }
}